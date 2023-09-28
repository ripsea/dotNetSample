using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest
{
    internal class JwtWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly ExternalServicesMock _externalServicesMock;

    public JwtWebApplicationFactory(ExternalServicesMock externalServicesMock)
        {
            _externalServicesMock = externalServicesMock;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.UseEnvironment("IntegrationTesting");
            //base.ConfigureWebHost(builder);

            builder
                .ConfigureServices(services =>
                {
                    foreach ((var interfaceType, var serviceMock) in _externalServicesMock.GetMocks())
                    {
                        services.Remove(services.SingleOrDefault(d => d.ServiceType == interfaceType));

                        services.AddSingleton(interfaceType, serviceMock);
                    }
                });
        }
    }
}
