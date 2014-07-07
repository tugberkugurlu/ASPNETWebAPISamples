using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AspNetTimeoutSample.MessageHandlers
{
    public class AspNetTimeoutHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpContext.Current.Server.ScriptTimeout = 20;
            return base.SendAsync(request, cancellationToken);
        }
    }
}