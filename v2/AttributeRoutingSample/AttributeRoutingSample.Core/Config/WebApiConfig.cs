using AttributeRoutingSample.Controllers;
using AttributeRoutingSample.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;
using WebApiDoodle.Web.Controllers;

namespace AttributeRoutingSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new InvalidModelStateFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Any complex type parameter which is assignable From 
            // IRequestCommand will be bound from the URI.
            config.ParameterBindingRules.Insert(0, descriptor =>
                typeof(IRequestCommand).IsAssignableFrom(descriptor.ParameterType)
                    ? new FromUriAttribute().GetBinding(descriptor) : null);

            // Replace the default action selector so that we can use 
            // complex types as URI parameters.
            config.Services.Replace(typeof(IHttpActionSelector), new ComplexTypeAwareActionSelector());
        }
    }
}
