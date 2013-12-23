using Owin;
using System.Web.Http;

namespace WebApiApp.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "{controller}");

            app.UseWebApi(config);
        }
    }
}