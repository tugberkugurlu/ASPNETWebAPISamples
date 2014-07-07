using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;

namespace TimeoutSample.MessageHandlers
{
    public class TimeoutHandler : DelegatingHandler
    {
        private const int RequestTimeoutInMinutes = 3;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Task<HttpResponseMessage> responseTask = await Task.WhenAny(
                WaitForTimeout(request),
                base.SendAsync(request, cancellationToken));

            return responseTask.Result;
        }

        private Task<HttpResponseMessage> WaitForTimeout(HttpRequestMessage request)
        {
            return Task.Run<HttpResponseMessage>(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(RequestTimeoutInMinutes));
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Request time out.");
            });
        }
    }
}