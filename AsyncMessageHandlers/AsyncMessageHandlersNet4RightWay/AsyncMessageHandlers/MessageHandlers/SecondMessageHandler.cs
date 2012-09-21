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

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken).Then(response => {

                int left = 10, right = 0;
                var result = left / right;

                return response;

            }).Catch<HttpResponseMessage>(info => {
                
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