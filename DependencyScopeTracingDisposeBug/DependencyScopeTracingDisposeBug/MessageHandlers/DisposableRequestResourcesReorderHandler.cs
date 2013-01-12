using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Hosting;

namespace DependencyScopeTracingDisposeBug.MessageHandlers {

    public class DisposableRequestResourcesReorderHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            return base.SendAsync(request, cancellationToken).Finally(() => {

                List<IDisposable> disposableResources = request.Properties[HttpPropertyKeys.DisposableRequestResourcesKey] as List<IDisposable>;
                if (disposableResources != null && disposableResources.Count > 1) {

                    // 1-) Get the first one (which I know is AutofacWebApiDependencyScope).
                    // 2-) Remove it from the list.
                    // 3-) Push it at the end of the list.

                    IDisposable dependencyScope = disposableResources[0];
                    disposableResources.RemoveAt(0);
                    disposableResources.Add(dependencyScope);
                }
            }, runSynchronously: true);
        }
    }
}