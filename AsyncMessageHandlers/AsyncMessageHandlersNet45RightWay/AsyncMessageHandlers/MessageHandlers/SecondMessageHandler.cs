using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AsyncMessageHandlers.MessageHandlers {

    public class SecondMessageHandler : DelegatingHandler {

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) {

                try {

                    int left = 10, right = 0;
                    var result = left / right;

                    var response = await base.SendAsync(request, cancellationToken);
                    return response;
                }
                catch (Exception ex) {

                    return request.CreateErrorResponse(
                        HttpStatusCode.InternalServerError, ex);
                }
        }
    }
}