using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Api.Models
{
    public class ApiResponse
    {
        public int code { get; private set; } = (int)HttpStatusCode.OK;

        public string message { get; private set; }

        public DataBox data { get; private set; }

        public ApiResponse()
        {
        }

        public ApiResponse(dynamic queryResult, int pageNumber, int pageSize, int totalPages, 
            int totalRecords)
        {
            data = new DataBox
            {
                requestId = string.Empty,
                rowsCount = 0,
                pagination = new Pagination(pageNumber: pageNumber,
                    pageSize: pageSize,
                    totalPages: totalPages,
                    totalRecords: totalRecords),
                rows = queryResult
            };
            message = "Success";
        }

        public ApiResponse(dynamic ex)
        {
            code = (int)HttpStatusCode.BadRequest;
            //message = ex.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}").Join(";");
            
        }

        public ApiResponse(UnauthorizedAccessException ex)
        {
            code = (int)HttpStatusCode.Unauthorized;
            message = ex.Message;
        }

        public class DataBox
        {
            public string requestId;
            public int rowsCount;
            public Pagination pagination { get; set; }
            public IEnumerable<dynamic> rows { get; set; }
        }
    }
}
