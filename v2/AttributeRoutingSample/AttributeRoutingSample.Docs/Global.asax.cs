using AttributeRoutingSample.Docs.Areas.HelpPage;
using AttributeRoutingSample.Docs.Areas.HelpPage.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AttributeRoutingSample.Docs
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // HelpPage config
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            HelpPageConfig.Register(config);

            config.EnsureInitialized();

            DependencyResolver.SetResolver(
                type => 
                {
                    if (type == typeof(HelpController))
                    {
                        return new HelpController(config);
                    }

                    return null;
                }, type => Enumerable.Empty<object>());
        }
    }
}