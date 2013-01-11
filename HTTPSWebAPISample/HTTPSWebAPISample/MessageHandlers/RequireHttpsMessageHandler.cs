using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HTTPSWebAPISample.MessageHandlers {

    public class RequireHttpsMessageHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) {

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps) {

                var forbiddenResponse =
                    request.CreateResponse(HttpStatusCode.Forbidden);

                forbiddenResponse.ReasonPhrase = "SSL Required";
                return Task.FromResult<HttpResponseMessage>(forbiddenResponse);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}