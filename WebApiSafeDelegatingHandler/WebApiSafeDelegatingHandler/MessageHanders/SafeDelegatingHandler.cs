using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiSafeDelegatingHandler.MessageHanders {

    public abstract class SafeDelegatingHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken).Catch<HttpResponseMessage>(info => {

                var cacthResult =
                    new CatchInfoBase<Task<HttpResponseMessage>>.CatchResult();

                cacthResult.Task = TaskHelpers.FromResult(
                    request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError, info.Exception));

                return cacthResult;
            });
        }
    }
}