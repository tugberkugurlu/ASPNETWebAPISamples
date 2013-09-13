using System.Net.Http;

namespace HostPrincipalServiceSample.Core
{
    public class UnauthenticatedRequestContext
    {
        public HttpRequestMessage Request { get; private set; }
        public HttpResponseMessage Response { get; set; }

        public UnauthenticatedRequestContext(HttpRequestMessage request)
        {
            Request = request;
        }
    }
}
