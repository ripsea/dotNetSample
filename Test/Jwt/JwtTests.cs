using Data.Entities;
using Data.Repositories.Base;
using Moq;
using Services.Models;
using Services.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JwtTest
{
    [TestFixture]
    public class JwtTests : JwtTestBase
    {

        [Test]
        public async Task GetTemperatureTest()
        {
            UserDto user = new UserDto() { Name = "iris", Password = "iris" };
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json");

            //var ttt = userServiceRepository.IsValidUserAsync(user);

            /*
            var expected = "test";
            ExternalServicesMock.UserServiceRepository
                .Setup(x => x.)
                .ReturnsAsync(expected);
            */
            /*


            var client = GetClient();

            var response = await client.PostAsync("/api/users/authenticate", content);
            var responseMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseMessage);
            */
            //Assert.AreEqual(expected, responseMessage);
            //ExternalServicesMock.TemperatureApiClient.Verify(x => x.GetTemperatureAsync(), Times.Once);
        }
    }
}
