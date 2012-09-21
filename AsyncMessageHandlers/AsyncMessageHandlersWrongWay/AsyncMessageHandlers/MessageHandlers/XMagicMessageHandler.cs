using System;
using System.Collections.Generic;
using System.Linq;
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
                .ContinueWith(task => {

                    //inspect the generated response
                    var response = task.Result;

                    //Add the X-Magic header
                    response.Headers.Add("X-Magic", "ThisIsMagic");

                    return response;
            });
        }
    }
}