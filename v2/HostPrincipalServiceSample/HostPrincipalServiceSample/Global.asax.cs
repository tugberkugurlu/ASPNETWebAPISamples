using HostPrincipalServiceSample.MessageHandlers;
using System;
using System.Web.Http;

namespace HostPrincipalServiceSample
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute("DefaultHttpRoute", "api/{controller}");
            config.MessageHandlers.Add(new AuthHandler());
        }
    }
}