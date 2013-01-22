using AtomPubSample.Hypermedia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AtomPubSample.MessageHandlers {

    public class EnrichingHandler : DelegatingHandler {
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            Collection<IResponseEnricher> enrichers = request.GetConfiguration().GetResponseEnrichers();
            return enrichers.Where(e => e.CanEnrich(response))
                .Aggregate(response, (resp, enricher) => enricher.Enrich(response));
        }
    }
}