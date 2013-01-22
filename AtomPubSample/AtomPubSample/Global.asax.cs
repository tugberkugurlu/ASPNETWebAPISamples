using AtomPubSample.Dispatchers;
using AtomPubSample.Formatters;
using AtomPubSample.Hypermedia;
using AtomPubSample.MessageHandlers;
using System;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Http;
using WebAPIDoodle.Filters;

namespace AtomPubSample {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.RegisterAtomPubServiceDocument("api/services");
            config.AddResponseEnrichers(new PostResponseEnricher());
            config.AddResponseEnrichers(new MediaResponseEnricher());
            config.RegisterRoutes();
            config.RegisterFilters();
            config.RegisterMessageHandlers();
            config.ConfigureFormatters();

            config.EnableSystemDiagnosticsTracing();
        }
    }

    internal static class AtopPubSampleConfigExtensions {

        internal static void RegisterRoutes(this HttpConfiguration config) {

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }

        internal static void RegisterFilters(this HttpConfiguration config) {

            config.Filters.Add(new InvalidModelStateFilterAttribute());
        }

        internal static void RegisterMessageHandlers(this HttpConfiguration config) {

            config.MessageHandlers.Add(new WLWMessageHandler());
            config.MessageHandlers.Add(new BasicAuthHandler());
            config.MessageHandlers.Add(new EnrichingHandler());
        }

        internal static void ConfigureFormatters(this HttpConfiguration config) {

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new AtomPubMediaFormatter());
            config.Formatters.Add(new AtomPubCategoryMediaTypeFormatter());
        }
    }
}