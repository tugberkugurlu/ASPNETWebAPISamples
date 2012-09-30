using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
using System.Web.SessionState;
using WebAPIDoodle.Controllers;

namespace ComplexTypeParamActionSelection {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultApiRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            // Replace the default action IHttpActionSelector with
            // WebAPIDoodle.Controllers.ComplexTypeAwareActionSelector
            config.Services.Replace(
                typeof(IHttpActionSelector),
                new ComplexTypeAwareActionSelector());
        }
    }
}