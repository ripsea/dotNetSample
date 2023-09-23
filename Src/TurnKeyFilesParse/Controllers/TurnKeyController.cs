using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using static System.Net.Mime.MediaTypeNames;
using Api;
using Api.Model;
using Api.Utilities;
using static Api.TurnKeyItemFactory;
using AspNetApi.Models.TurnKeyItem;

namespace TurnKeyFilesAPI
{

    public class TurnKeyController : ApiController
    {
        // GET api/v1/e0501/Json/{id}
        [HttpGet]
        //[MyAuthorize(Roles = "Admin")]
        [Route("api/v1/{item}/json/{id}")]
        public async Task<IHttpActionResult> GetJson(TurnKeyItemEnum item, int id)
        {
            id = (id == 0) ? 1 : id;
            if (id > 180)
            {
                return this.BadRequest("180 days limited.");
            }

            ITurnKeyService turnKeyItem;
            IEnumerable<string> filesPathString;
            IEnumerable<FileInfoData> files;
            IEnumerable<FileListResponse> responses;

            turnKeyItem
            = (ITurnKeyService)TurnKeyItemFactory.GetTurnKeyItem(item);

            filesPathString 
                = turnKeyItem.GetTargetFilesPathString(
                    turnKeyItem.GetTurnKeyFile());

            files
            = turnKeyItem
            .GetFileInfoAndPathSegmentAndXmlAndSetQueryDate(filesPathString)
            .FindByDuration(
                DateTime.Now.AddDays(id * -1)
                , DateTime.Now);

            responses = turnKeyItem.GetResponse(files);

            return await Task.FromResult(this.Ok(responses));
        }

        // GET api/v1/e0501/Xlsx/{id}
        [HttpGet]
        [Route("api/v1/e0501/xlsx/{id}")]
        public async Task<IHttpActionResult> GetXlsx(int id)
        {
            var ttt = GetJson(TurnKeyItemEnum.E0501,id);
            return await Task.FromResult(ttt.Result);
        }
    }
}
