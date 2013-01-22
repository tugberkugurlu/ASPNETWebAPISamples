using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AtomPubSample.MessageHandlers {

    public class WLWMessageHandler : DelegatingHandler {

        private const string WLWUserAgent = "Windows Live Writer 1.0";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            var headers = request.Headers.ToString();
            var contHeaders = request.Headers.ToString();
            var content = request.Content.ReadAsStringAsync().Result;

            if (request.Headers.UserAgent != null &&
                request.Headers.UserAgent.Any(a => a.Comment != null && a.Comment.Contains(WLWUserAgent))) {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/atom+xml"));
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}