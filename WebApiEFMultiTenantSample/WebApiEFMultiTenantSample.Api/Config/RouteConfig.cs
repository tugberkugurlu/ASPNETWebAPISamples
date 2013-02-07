using System.Web.Http;

namespace WebApiEFMultiTenantSample.Api.Config {
    
    public static class RouteConfig {

        public static void RegisterRoutes(HttpConfiguration config) {

            var routes = config.Routes;

            routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{tenant}/{controller}/{key}",
                defaults: new { key = RouteParameter.Optional });
        }
    }
}