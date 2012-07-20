using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace TCSCachesInsideMessageHandlers {

    public class MyFirstHandler : DelegatingHandler {

        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {
            
            var response = await base.SendAsync(request, cancellationToken);
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            return response;
        }
    }
}