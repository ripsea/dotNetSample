﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;

namespace Client
{
    [TestClass]
    public class UnitTest1
    {
        private const string HOST_ADDRESS = "http://localhost:8002";
        private static IDisposable s_webApp;
        private static HttpClient s_client;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            s_webApp = WebApp.Start<Startup>(HOST_ADDRESS);
            Console.WriteLine("Web API started!");
            s_client = new HttpClient();
            s_client.BaseAddress = new Uri(HOST_ADDRESS);
            Console.WriteLine("HttpClient started!");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            s_webApp.Dispose();
        }
        [TestMethod]
        public void Access_Resource_HttpStatus_Should_Be_Unauthorized()
        {
            var queryUrl = "api/value";

            var queryResponse = s_client.GetAsync(queryUrl).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, queryResponse.StatusCode);
        }

        [TestMethod]
        public void Access_Resource_Should_Be_Value()
        {
            var loginUrl = "api/token";
            var queryUrl = "api/value";

            var tokenResponse = s_client.PostAsJsonAsync(loginUrl,
                                                         new LoginData
                                                         {
                                                             UserName = "yao",
                                                             Password = "1234"
                                                         })
                                        .Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResponse.StatusCode);

            var token = tokenResponse.Content.ReadAsStringAsync().Result;
            s_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var queryResponse = s_client.GetAsync(queryUrl).Result;
            Assert.AreEqual(HttpStatusCode.OK, queryResponse.StatusCode);
            var result = queryResponse.Content.ReadAsStringAsync().Result;
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Token_Expired_Should_Be_Unauthorized()
        {
            var loginUrl = "api/token";
            var queryUrl = "api/value";

            //注入期望時間
            JwtManager.Now = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc);

            var tokenResponse = s_client.PostAsJsonAsync(loginUrl,
                                                         new LoginData
                                                         {
                                                             UserName = "yao",
                                                             Password = "1234"
                                                         })
                                        .Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResponse.StatusCode);

            var token = tokenResponse.Content.ReadAsStringAsync().Result;
            s_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //模擬時間經過30分鐘
            JwtManager.Now = JwtManager.Now.Value.AddMinutes(30);
            var queryResponse = s_client.GetAsync(queryUrl).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, queryResponse.StatusCode);
            JwtManager.Now = null;
        }

        [TestMethod]
        public void Token_NotBefore_Should_Be_Unauthorized()
        {
            var loginUrl = "api/token";
            var queryUrl = "api/value";

            //注入Token可以使用的時間
            JwtManager.Now = DateTime.SpecifyKind(new DateTime(2000, 1, 2), DateTimeKind.Utc);

            var tokenResponse = s_client.PostAsJsonAsync(loginUrl,
                                                         new LoginData
                                                         {
                                                             UserName = "yao",
                                                             Password = "1234"
                                                         })
                                        .Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResponse.StatusCode);

            var token = tokenResponse.Content.ReadAsStringAsync().Result;
            s_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //注入目前時間
            JwtManager.Now = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc);
            var queryResponse = s_client.GetAsync(queryUrl).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, queryResponse.StatusCode);
            JwtManager.Now = null;
        }

        public class LoginData
        {
            public string UserName { get; set; }

            public string Password { get; set; }
        }
    }
}