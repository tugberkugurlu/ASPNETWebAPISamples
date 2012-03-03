using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using ConnegAlgorithmSample.Formatting;

namespace ConnegAlgorithmSample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "defaultHttpRoute",
                routeTemplate: "api/{controller}.{extension}",
                defaults: new { },
                constraints: new { extension = "json|xml" }
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.
                MediaTypeMappings.Add(
                    new QueryStringMapping(
                        "format", "json", "application/json"
                )
            );

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.
                MediaTypeMappings.Add(
                    new QueryStringMapping(
                        "format", "xml", "application/xml"
                )
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.
                MediaTypeMappings.Add(
                    new RouteDataMapping(
                        "extension", "json", "application/json"
                )
            );

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.
                MediaTypeMappings.Add(
                    new RouteDataMapping(
                        "extension", "xml", "application/xml"
                )
            );
        }
   }
}