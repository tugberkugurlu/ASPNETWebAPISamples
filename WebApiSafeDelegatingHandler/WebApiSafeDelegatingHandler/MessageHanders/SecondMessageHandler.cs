using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiSafeDelegatingHandler.MessageHanders {

    public class SecondMessageHandler : SafeDelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken).Then(response => {

                int left = 10, right = 0;
                var result = left / right;
                return response;
            });
        }
    }
}