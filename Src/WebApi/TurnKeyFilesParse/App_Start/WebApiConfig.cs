using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Api;
using Api.Models;
using Api.Models.Provider;
using System.Web.Http.Cors;
using Api.Models.Handler;

namespace AspNetApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional }
            );

            // 錯誤處理
            // config.MessageHandlers.Add(new WebApiCustomMessageHandler());
            config.MessageHandlers.Add(new WrappingHandler());
            // Log
            config.MessageHandlers.Add(new LogMessageHandler());

            // Enable CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // 保留回傳 xml 能力，預設回傳 json
            var appXmlType
                = config.Formatters.XmlFormatter.SupportedMediaTypes
                    .FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // JSON 縮排換行
            config.Formatters.JsonFormatter.SerializerSettings.Formatting
                = Newtonsoft.Json.Formatting.Indented;


        }
    }
}
