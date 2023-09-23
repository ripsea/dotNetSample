using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NSubstitute;
using Services.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest
{
    internal class UserTestServer : WebApplicationFactory<Program>
    {
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(p =>
            {
                var userService = Substitute.For<IUserServiceRepository>();
                userService.AddUserRefreshTokens().RefreshToken
                return userService;
            });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(this.ConfigureServices);
        }
    }
}
