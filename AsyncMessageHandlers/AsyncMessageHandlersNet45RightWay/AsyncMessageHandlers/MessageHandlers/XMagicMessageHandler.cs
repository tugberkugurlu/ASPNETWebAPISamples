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

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken) {

            try {

                var response = await base.SendAsync(request, cancellationToken);
                response.Headers.Add("X-Magic", "ThisIsMagic");
                return response;
            }
            catch (Exception ex) {

                return request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}