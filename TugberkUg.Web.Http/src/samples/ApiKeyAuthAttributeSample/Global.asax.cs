using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using ApiKeyAuthAttributeSample.Infrastructure;
using TugberkUg.Web.Http.Filters;

namespace ApiKeyAuthAttributeSample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "defaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            //You can register the ApiKeyAuthAttribute globaly so that it applies all of your app
            //GlobalConfiguration.Configuration.Filters.Add(new ApiKeyAuthAttribute("apiKey", typeof(InMemoryApiKeyAuthorizer)));
        }
    }
}