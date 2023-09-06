using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Api.Model;
using Api.Model.TurnKeyItem;
using Api.Model.TurnKeyItem.E0501;
using Api.Models.TurnKeyItem.SummaryResult;
using Api.Utilities;
using static Api.TurnKeyItemFactory;

namespace Api
{
    public class TurnKeyFile
    {

        public TurnKeyFile(TurnKeyItemEnum itemId
            , string targetFolder
            , int targetFolderLayer = 0
            , DirectorieOrFileTypeEnum searchTarget = DirectorieOrFileTypeEnum.File
            , Regex regex = null
            )
        {
            this.ItemID = itemId;
            this.CacheKey = itemId.ToString();
            this.TargetFolder = targetFolder;
            this.TargetFolderLayer = targetFolderLayer;
        }

        public TurnKeyItemEnum ItemID { get; set; }
        public string CacheKey { get; set; }
        public string TargetFolder { get; set; }
        public int TargetFolderLayer { get; set; }
        public DirectorieOrFileTypeEnum TargetType { get; set; }
        public Regex TargetNameRegex { get; set; }
        public Regex TargetCheckNameRegex { get; set; }
        public IEnumerable<string> ErrorMessage { get; set; } = new List<string>();

        public IList<string> TryGetFilePathSegment(string filePath
            , out bool errCheck
            , out string errMessage)
        {
            IList<string> filePathSegment = new List<string>();
            errMessage = string.Empty;
            errCheck = false;

            if (!File.Exists(filePath))
            {
                errMessage = $"{filePath} 檔案不存在.";
                Logger.Warn(errMessage);
                return filePathSegment;
            }

            filePathSegment
                = Utility.GetDirectorySegments(filePath);

            if (filePathSegment == null
                || filePathSegment.Count < this.TargetFolderLayer)
            {
                errMessage = $"{filePathSegment}取得檔案路徑Segment失敗.預期有{this.TargetFolderLayer}個Segment.";
                Logger.Warn(errMessage);
                return filePathSegment;
            }

            errCheck = true;
            return filePathSegment;
        }

        public bool TryRegex(Regex regex, string fileName)
        {
            if (regex != null)
            {
                return regex.IsMatch(fileName);
            }
            return true;

        }
    }

    public enum DirectorieOrFileTypeEnum
    {
        File,
        Folder,
        All
    }

    public enum TurnKeyItemEnum
    {
        E0501,
        SummaryResult,
        ProcessResult
    }

    public class FileInfoData
    {

        public FileInfoData(FileInfo file, 
            IList<string> filePathSegment,
            object xmlObject
            )
        {
            File = file;
            FilePathSegment = filePathSegment;
            XmlObject = xmlObject;

            string[] fileNameSplit = file.Name.Split('-');
            if (fileNameSplit.Count() == 4)
            {
                this.FileVersion = fileNameSplit[0];
                this.FileApplyBusinessID = fileNameSplit[1];
                this.FileDate = fileNameSplit[2];
                this.FileSequence = fileNameSplit[3];
            }

            DateTime ttt;
            DateTime.TryParseExact(
                this.FileDate
                , "yyyyMMdd"
                , null
                , DateTimeStyles.None
                , out ttt);

            QueryYYYYMMDD = ttt;
        }

        public FileInfo File { get; set; }
        public IList<string> FilePathSegment { get; set; }
        public object XmlObject { get; set; }
        public DateTime QueryYYYYMMDD { get; set; }
        public string FileVersion { get; set; } = string.Empty;
        public string FileApplyBusinessID { get; set; } = string.Empty;
        public string FileDate { get; set; } = string.Empty;
        public string FileSequence { get; set; } = string.Empty;
    }
}