using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest
{
    internal class JwtBaseTest
    {
        private readonly JwtWebApplicationFactory _webApplicationFactory;
        public ExternalServicesMock ExternalServicesMock { get; }

        public JwtBaseTest()
        {
            ExternalServicesMock = new ExternalServicesMock();
            _webApplicationFactory = new JwtWebApplicationFactory(ExternalServicesMock);
        }

        public HttpClient GetClient() => _webApplicationFactory.CreateClient();
    }
}
