using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TugberkUg.Web.Http.MessageHandlers {

    //https://gist.github.com/1881538
    public class IWantJSONDammit : DelegatingHandler {

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {

                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return base.SendAsync(request, cancellationToken);
        }
    }
}