using JWT.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Jwt.Swagger
{
    internal class UserViewModelRequestExample :
        IExamplesProvider<UserViewModel>
    {
        public UserViewModel GetExamples()
        {
            return new UserViewModel
            {
                Name = "iris",
                Password = "iris"
            };
        }
    }
}