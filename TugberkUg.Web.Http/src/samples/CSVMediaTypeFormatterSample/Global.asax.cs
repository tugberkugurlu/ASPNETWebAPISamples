using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using TugberkUg.Web.Http.Formatting;
using TugberkUg.Web.Http.MessageHandlers;
using System.Net.Http.Formatting;

namespace CSVMediaTypeFormatterSample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{ext}",
                new { ext = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Formatters.Add(new CSVMediaTypeFormatter(new  QueryStringMapping("format", "csv", "text/csv")));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new UriFormatExtensionHandler(new UriExtensionMappings()));
        }
    }
}