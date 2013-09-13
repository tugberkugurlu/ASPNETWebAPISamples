using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HypermediaIntro.MessageHandlers {

    public class EnrichingHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken).Then(response => {

                Collection<IResponseEnricher> enrichers = request.GetConfiguration().GetResponseEnrichers();
                return enrichers.Where(e => e.CanEnrich(response))
                    .Aggregate(response, (resp, enricher) => enricher.Enrich(resp));
            });
        }
    }
}