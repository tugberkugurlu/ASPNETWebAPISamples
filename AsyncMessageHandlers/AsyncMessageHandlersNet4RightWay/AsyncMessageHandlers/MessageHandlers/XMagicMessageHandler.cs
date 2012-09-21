using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AsyncMessageHandlers.MessageHandlers {
    
    public class XMagicMessageHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken) {
            
            //Play with the request here

            return base.SendAsync(request, cancellationToken)
                .Then(response => {

                    //Add the X-Magic header
                    response.Headers.Add("X-Magic", "ThisIsMagic");
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