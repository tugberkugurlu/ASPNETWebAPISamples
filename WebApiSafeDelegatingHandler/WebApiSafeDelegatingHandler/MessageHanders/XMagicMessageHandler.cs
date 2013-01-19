using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiSafeDelegatingHandler.MessageHanders {

    public class XMagicMessageHandler : SafeDelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) {

            // Play with the request here

            return base.SendAsync(request, cancellationToken).Then(response => {

                //Add the X-Magic header
                response.Headers.Add("X-Magic", "ThisIsMagic");

                return response;
            });
        }
    }
}