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

                return base.SendAsync(request, cancellationToken).ContinueWith(task => {

                    throw new DivideByZeroException();

                    return new HttpResponseMessage();
                });
        }
    }
}