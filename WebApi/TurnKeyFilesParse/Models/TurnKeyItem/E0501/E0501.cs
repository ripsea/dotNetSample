using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Compilation;
using System.Xml;
using System;
using System.Linq;
using Api.Model.TurnKeyItem.E0501;
using Api.Model;
using Api.Utilities;
using Api;
using AspNetApi.Models.TurnKeyItem;

namespace Api.Models.E0501
{
    public class E0501 : ITurnKeyService
    {
        TurnKeyFile _turnKeyFile;
        public E0501(TurnKeyFile turnKeyFile)
        {
            _turnKeyFile = turnKeyFile;
        }

        public TurnKeyFile GetTurnKeyFile() { return _turnKeyFile; }

        public DateTime TryGetFolderPathYYYYMMDDSegment(
            IList<string> filePathSegment,
            out bool errCheck,
            out string errMessage)
        {
            errMessage = string.Empty;
            errCheck = false;
            DateTime directoryYYYYMMDDSegment;

            DateTime.TryParseExact(
                filePathSegment[5]
                , "yyyyMMdd"
                , null
                , DateTimeStyles.None
                , out directoryYYYYMMDDSegment);

            if (directoryYYYYMMDDSegment == DateTime.MinValue)
            {
                errMessage = $"{filePathSegment[5]}在取得檔案directoryYYYYMMDDSegment={directoryYYYYMMDDSegment}, 轉換日期失敗";
                //Logger.Warn(errMessage);
            }

            errCheck = true;
            return directoryYYYYMMDDSegment;
        }

        public IEnumerable<FileListResponse> GetResponse(
            IEnumerable<FileInfoData> files)
        {

            List<FileListResponse> responses = new List<FileListResponse>();
            List<object> details = new List<object>();

            foreach (var file in files)
            {
                InvoiceEnvelope e0501 = file.XmlObject as InvoiceEnvelope;
                FileListResponse response
                    = new FileListResponse(new E0501Master(
                        fromPartyId: e0501.From.PartyId,
                        fromVACRoutingId: e0501.FromVAC.RoutingId,
                        e0501.To.PartyId,
                        e0501.ToVAC.RoutingId,
                        file.FileVersion,
                        file.FileApplyBusinessID,
                        file.FileDate,
                        file.FileSequence
                        ));
                details = new List<object>();
                foreach (var trackDetail in e0501.InvoicePack.InvoiceAssignNo)
                {
                    E0501Detail detail = new E0501Detail
                        (
                        sellerId: e0501.From.PartyId
                        , trackID: trackDetail.InvoiceTrack
                        , yearMonth: trackDetail.YearMonth
                        , startNo: trackDetail.InvoiceBeginNo
                        , endNo: trackDetail.InvoiceEndNo
                        , booklet: trackDetail.InvoiceBooklet);
                    details.Add(detail);
                }
                response.Details = details;
                responses.Add(response);
            }

            return responses;
        }

        public IEnumerable<FileInfoData> GetFileInfoAndPathSegmentAndXmlAndSetQueryDate(
            IEnumerable<string> filesPath)
        {
            List<FileInfoData> e0501FileInfoDatas = new List<FileInfoData>();
            List<string> errorMessage = new List<string>();
            foreach (string filePath in filesPath)
            {
                bool isError;
                string checkMessage;
                IList<string> filePathSegment;
                filePathSegment = _turnKeyFile.TryGetFilePathSegment(
                    filePath: filePath,
                    out isError,
                    out checkMessage);

                if (!isError)
                {
                    errorMessage.Add(checkMessage);
                    break;
                }

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                InvoiceEnvelope e0501 = xmlDocument.ConvertTo<InvoiceEnvelope>();
                e0501FileInfoDatas.Add(new FileInfoData(
                    new FileInfo(filePath),
                    filePathSegment: filePathSegment,
                    e0501
                    ));

            }

            return e0501FileInfoDatas.AsEnumerable<FileInfoData>();
        }

        public IEnumerable<string> GetTargetFilesPathString(
            TurnKeyFile turnKeyFile)
        {
            return Utility.EnumerateSubDirectoriesOrFiles(
                    turnKeyFile.TargetFolder
                    , turnKeyFile.TargetFolderLayer
                    , turnKeyFile.TargetType);
        }
    }
}