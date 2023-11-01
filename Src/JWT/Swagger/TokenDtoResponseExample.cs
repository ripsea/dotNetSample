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
                AccessToken = "thisisaccesstoken",
                RefreshToken = "thisisarefreshtoken"
            };
        }
    }
}