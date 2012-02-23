using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ElmahErrorFilterApp.Infrastructure.Filters;

namespace ElmahErrorFilterApp {

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication {

        protected void Application_Start() {

            Configure(System.Web.Http.GlobalConfiguration.Configuration);
        }

        private void Configure(HttpConfiguration httpConfiguration) {

            httpConfiguration.Filters.Add(new ElmahErrorAttribute());

            httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}