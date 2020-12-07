using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RhNetServer.App_Start;
using RhNetServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RhNetServer.Security;
using System.Web.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;

[assembly: OwinStartup(typeof(RhNetServer.Startup))]
namespace RhNetServer
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);


            ConfigureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);



        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(RhNetContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions()
            {               
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:4200")
            };
            app.UseOAuthAuthorizationServer(authServerOptions);

            var issuer = "http://localhost:4200";
            var audience = WebApplicationAccess.WebApplicationAccessList.Select(x => x.Value.ClientId).AsEnumerable();

            var secretsSymmetricKey = (from x in WebApplicationAccess.WebApplicationAccessList
                                       select new SymmetricKeyIssuerSecurityKeyProvider(issuer, TextEncodings.Base64Url.Decode(x.Value.SecretKey))).ToArray();

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AllowedAudiences = audience,
                    IssuerSecurityKeyProviders = secretsSymmetricKey,
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
                });

        }
    }
}