using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DependencyScopeTracingDisposeBug.MessageHandlers {

    public class UserHostAddressSetterHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            request.Properties[ApiCommonRequestKeys.MS_UserHostAddressKey] = request.GetUserHostAddress();
            return base.SendAsync(request, cancellationToken);
        }
    }
}