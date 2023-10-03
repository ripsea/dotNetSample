using Services.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Jwt.Swagger
{
    internal class TokenDtoResponseExample : IExamplesProvider<TokenDto>
    {
        public TokenDto GetExamples()
        {
            return new TokenDto
            {
                Access_Token = "thisisaccesstoken",
                Refresh_Token = "thisisarefreshtoken"
            };
        }
    }
}