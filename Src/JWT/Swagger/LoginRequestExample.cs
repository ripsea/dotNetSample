using Jwt.Models;
using JWT.Models;
using Services.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Jwt.Swagger
{
    internal class LoginRequestExample : IExamplesProvider<LoginRequest>
    {

            public LoginRequest GetExamples()
            {
                return new LoginRequest
                {
                    Name = "iris",
                    Password = "!QAZ2wsx"
                };
            }
    }
}