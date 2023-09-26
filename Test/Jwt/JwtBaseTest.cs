using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest
{
    public class JwtBaseTest
    {
        private readonly JwtWebApplicationFactory _webApplicationFactory;
        public ExternalServicesMock externalServicesMock { get; }

        public JwtBaseTest()
        {
            externalServicesMock = new ExternalServicesMock();
            _webApplicationFactory = new JwtWebApplicationFactory(externalServicesMock);
        }

        public HttpClient GetClient() => _webApplicationFactory.CreateClient();
    }
}
