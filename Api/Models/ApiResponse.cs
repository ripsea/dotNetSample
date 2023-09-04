using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Models
{
    public class ApiResponse
    {
        public string Version { get { return "1.2.3"; } }

        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }
        public DateTime DateTime { get => DateTime.Now; }

        public ApiResponse(HttpStatusCode statusCode, 
            object result = null, 
            string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
