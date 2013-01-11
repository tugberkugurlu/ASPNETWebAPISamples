using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Security.Principal;

namespace AuthorizeAttributeSample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Filters.Add(new AuthorizeAttribute());
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) { 

            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null) {

                FormsAuthenticationTicket authTicket = 
                    FormsAuthentication.Decrypt(authCookie.Value);

                string[] roles = authTicket.UserData.Split(',');

                GenericPrincipal userPrincipal =
                    new GenericPrincipal(
                        new GenericIdentity(authTicket.Name), roles);

                Context.User = userPrincipal;
            }
        }
    }
}