using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiSafeDelegatingHandler.MessageHanders {

    public class ContShouldRunIfRunToCompMessageHandler : SafeDelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken).Then(response => {

                // With SafeDelegatingHandler, you only need to check the StatusCode
                // of the response and act on it instead of checking the 
                // task status as you will never get the faulted task.
                if (response.IsSuccessStatusCode) {

                    response.Headers.Add("X-StatusCode", "Success");
                }

                return response;
            });
        }
    }
}