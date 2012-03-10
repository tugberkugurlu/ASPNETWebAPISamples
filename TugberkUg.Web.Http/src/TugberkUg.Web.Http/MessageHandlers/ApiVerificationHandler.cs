using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TugberkUg.Web.Http.MessageHandlers {

    internal class ApiVerificationHandler : DelegatingHandler {

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
            System.Threading.CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken);
        }
    }
}