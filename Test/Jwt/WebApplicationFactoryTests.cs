using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Jwt
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WebApplicationFactory基本用法()
        {
            var server = new WebApplicationFactory<Program>();
            var client = server.CreateClient();
            var url = "api/users";
            var response = client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }
    }
}