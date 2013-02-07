using Autofac;
using Autofac.Extras.Multitenant;
using Autofac.Integration.WebApi;
using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiEFMultiTenantSample.Api.Services;

namespace WebApiEFMultiTenantSample.Api.Config {
    
    public static class AutofacWebApi {

        public static void Initialize(HttpConfiguration config, ITenantIdentificationStrategy idStrategy) {

            Initialize(config,
                RegisterServices(new ContainerBuilder(), idStrategy));
        }

        public static void Initialize(HttpConfiguration config, IContainer container) {

            config.DependencyResolver =
                new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder, ITenantIdentificationStrategy idStrategy) {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => idStrategy).As<ITenantIdentificationStrategy>();
            builder.Register(c => {
                
                ITenantIdentificationStrategy s = c.Resolve<ITenantIdentificationStrategy>();
                object tenantId;
                s.TryIdentifyTenant(out tenantId);
                return tenantId;

            }).Keyed<object>("tenantId");

            // creates a logger instance per tenant
            builder.RegisterType<LoggerService>()
                .As<ILoggerService>().WithParameter((pi, c) => pi.Name == "tenant",
                    (pi, c) => c.ResolveKeyed<object>("tenantId")).InstancePerTenant();

            MultitenantContainer mtc = new MultitenantContainer(
                idStrategy, builder.Build());

            return mtc;
        }
    }
}