using Owin;
using System.Threading;
using System.Web.Http;

namespace KatanaOWINWebApiSample {
    
    public class Startup {

        public void Configuration(IAppBuilder builder) {

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
            
            builder.UseHttpServer(config);
        }
    }
}