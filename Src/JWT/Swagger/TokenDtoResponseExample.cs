using Services.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Jwt.Swagger
{
    internal class TokenDtoResponseExample : IExamplesProvider<TokenRequest>
    {
        public TokenRequest GetExamples()
        {
            return new TokenRequest
            {
                Access_Token = "thisisaccesstoken",
                Refresh_Token = "thisisarefreshtoken"
            };
        }
    }
}