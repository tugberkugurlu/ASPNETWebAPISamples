using Autofac.Extras.Multitenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Autofac.Builder;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Autofac.Extras.Multitenant.Web;
using MultiTenantWebAPI.Services;

namespace MultiTenantWebAPI {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute("Default", "api/{controller}");
            RegisterDependencies(config);
        }

        public void RegisterDependencies(HttpConfiguration config) {

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // creates a logger instance per tenant
            builder.RegisterType<LoggerService>().As<ILoggerService>().InstancePerTenant();

            var mtc = new MultitenantContainer(
                new RequestParameterTenantIdentificationStrategy("tenant"),
                builder.Build());

            config.DependencyResolver = new AutofacWebApiDependencyResolver(mtc);
        }
    }
}