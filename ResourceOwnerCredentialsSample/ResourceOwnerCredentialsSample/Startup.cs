using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;

namespace ResourceOwnerCredentialsSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            // OAuth Server
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions 
            {
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                Provider = new SimpleOAuthProvider()
            });

            // Authentication Middleware to bring up the identity of the caller.
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

    public class SimpleOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuth2 supports the notion of client authentication. This is not used here.
            context.Validated();
            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.Password == context.UserName)
            {
                ClaimsIdentity id = new ClaimsIdentity("Embedded");
                id.AddClaim(new Claim("sub", context.UserName));
                id.AddClaim(new Claim(ClaimTypes.Role, "user"));
                
                context.Validated(id);
            }
            else
            {
                context.Rejected();
            }

            return Task.FromResult(0);
        }
    }
}