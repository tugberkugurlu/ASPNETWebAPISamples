using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using JsonPatch.Formatting;

namespace JsonPatchSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // refer to: http://michael-mckenna.com/Blog/how-to-add-json-patch-support-to-asp-net-web-api
            config.Formatters.Add(new JsonPatchFormatter());
        }
    }
}
