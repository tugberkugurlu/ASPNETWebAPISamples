using Autofac;
using Autofac.Integration.WebApi;
using ConstructControllerSeperatelySample.Controllers;
using ConstructControllerSeperatelySample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace ConstructControllerSeperatelySample {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            // IoC stuff
            var builder = new ContainerBuilder();
            
            // Uncomment if you have other controllers
            // other than the below registered two.
            // builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Loggers
            builder.RegisterType<DbLogger>().Keyed<ILogger>(typeof(DbLogger)).InstancePerApiRequest();
            builder.RegisterType<TraceLogger>().Keyed<ILogger>(typeof(TraceLogger)).InstancePerApiRequest();

            // Register controller specifically.
            builder.Register(c => {
                var logger = c.ResolveKeyed<ILogger>(typeof(DbLogger));
                return new CarsController(logger);
            }).As<CarsController>();

            builder.Register(c => {
                var logger = c.ResolveKeyed<ILogger>(typeof(TraceLogger));
                return new ValuesController(logger);
            }).As<ValuesController>();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}