using Autofac.Extras.Multitenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using WebApiEFMultiTenantSample.Api.Config;
using WebApiEFMultiTenantSample.Api.WebHost.MessageHandlers;

namespace WebApiEFMultiTenantSample.Api.WebHost {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            HttpConfiguration config = GlobalConfiguration.Configuration;
            ITenantIdentificationStrategy idStrategy = 
                new ContextItemsTenantIdentificationStrategy("App:Tenant");

            config.MessageHandlers.Add(new TenantIdentifierMessageHandler());

            AutofacWebApi.Initialize(config, idStrategy);
            WebApiConfig.Configure(config);
            RouteConfig.RegisterRoutes(config);
        }
    }
}