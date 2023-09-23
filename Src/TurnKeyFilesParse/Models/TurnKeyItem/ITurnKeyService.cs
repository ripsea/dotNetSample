using Api.Model;
using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models;

namespace AspNetApi.Models.TurnKeyItem
{
    public interface ITurnKeyService
    {
        TurnKeyFile GetTurnKeyFile();

        DateTime TryGetFolderPathYYYYMMDDSegment(
            IList<string> filePathSegment
            , out bool errCheck
            , out string errMessage);

        IEnumerable<string> GetTargetFilesPathString(
            TurnKeyFile turnKeyFile);


        IEnumerable<FileInfoData> GetFileInfoAndPathSegmentAndXmlAndSetQueryDate(
            IEnumerable<string> filesPathList);

        IEnumerable<FileListResponse> GetResponse(
            IEnumerable<FileInfoData> files);
    }
}
