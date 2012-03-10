using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace TugberkUg.Web.Http.MessageHandlers {

    //https://github.com/thinktecture/Thinktecture.Web.Http/blob/master/Thinktecture.Web.Http/Handlers/LoggingHandler.cs
    public class TraceLoggingHandler : DelegatingHandler {

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) {

            Trace.TraceInformation("Begin Request: {0} {1}", request.Method, request.RequestUri);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
