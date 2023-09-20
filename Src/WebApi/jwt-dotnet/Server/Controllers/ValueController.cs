using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Server.Controllers
{
    public class ValueController : ApiController
    {
        //沒有設定[AllowAnonymous], 進Controller前先過JwtAuthorizeAttribute的Token檢查
        public IHttpActionResult Get()
        {
            return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("value")
            });
        }
    }
}