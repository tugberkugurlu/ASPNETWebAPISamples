using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace TCSCachesInsideMessageHandlers {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            GlobalConfiguration.Configuration.Routes.MapHttpRoute("Default", "api/{controller}");
            GlobalConfiguration.Configuration.MessageHandlers.Add(new EverythingIsEvilHandler());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new MyFirstHandler());
        }
    }
}