using Autofac;
using DependencyScopeTracingDisposeBug.Models.Entities;
using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Autofac.Integration.WebApi;
using System.Reflection;
using DependencyScopeTracingDisposeBug.Services;
using DependencyScopeTracingDisposeBug.MessageHandlers;
using System.Web.Http.Tracing;
using DependencyScopeTracingDisposeBug.Tracing;

namespace DependencyScopeTracingDisposeBug {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            RegisterTypes(config);
            config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());

            // In order to workaround the IDependencyScope and tracing issue,
            // uncomment the below line.
            // config.MessageHandlers.Add(new DisposableRequestResourcesReorderHandler());
            config.MessageHandlers.Add(new UserHostAddressSetterHandler());
        }

        private void RegisterTypes(HttpConfiguration config) {

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(_ => new WebApiTracerContext()).As<IEntitiesContext>().InstancePerApiRequest();
            builder.RegisterType<EntityRepository<HttpApiLogRecord>>().As<IEntityRepository<HttpApiLogRecord>>().InstancePerApiRequest();
            builder.RegisterType<LoggerService>().As<ILoggerService>().InstancePerApiRequest();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}