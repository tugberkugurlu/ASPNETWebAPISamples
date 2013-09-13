using HypermediaIntro.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace HypermediaIntro {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                Constants.DefaultHttpRoute,
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new EnrichingHandler());
            config.AddResponseEnrichers(new CarResponseEnricher());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}