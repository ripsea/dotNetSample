using Api.Models.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(AspNetApi.Startup))]

namespace AspNetApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //Token Auth
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                TokenEndpointPath = new PathString("/token"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new AuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
