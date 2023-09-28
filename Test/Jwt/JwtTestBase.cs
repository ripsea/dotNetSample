using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTest
{
    public class JwtTestBase
    {
        private readonly JwtWebApplicationFactory _WebApplicationFactory;
        public ExternalServicesMock ExternalServicesMock { get; }

        public JwtTestBase()
        {
            ExternalServicesMock = new ExternalServicesMock();
            _WebApplicationFactory = 
                new JwtWebApplicationFactory(ExternalServicesMock);
        }

        public HttpClient GetClient() => _WebApplicationFactory.CreateClient();
    }
}
