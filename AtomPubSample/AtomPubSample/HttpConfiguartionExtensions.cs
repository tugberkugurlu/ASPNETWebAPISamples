using AtomPubSample.Dispatchers;
using AtomPubSample.Hypermedia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AtomPubSample {

    public static class HttpConfiguartionExtensions {

        public static void RegisterAtomPubServiceDocument(this HttpConfiguration config, string path) {

            config.Routes.MapHttpRoute(
                "__AtomPubServicesRoute",
                path,
                defaults: null,
                constraints: null,
                handler: new AtomPubServiceDocumentDispatcher()
            );
        }

        public static void AddResponseEnrichers(this HttpConfiguration config, params IResponseEnricher[] enrichers) {

            foreach (var enricher in enrichers) {
                config.GetResponseEnrichers().Add(enricher);
            }
        }

        public static Collection<IResponseEnricher> GetResponseEnrichers(this HttpConfiguration config) {

            return (Collection<IResponseEnricher>)config.Properties.GetOrAdd(
                    typeof(Collection<IResponseEnricher>),
                    k => new Collection<IResponseEnricher>()
                );
        }
    }
}