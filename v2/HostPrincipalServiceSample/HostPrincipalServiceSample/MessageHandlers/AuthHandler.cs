using HostPrincipalServiceSample.Core.MessageHandlers;
using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HostPrincipalServiceSample.MessageHandlers
{
    public class AuthHandler : BasicAuthenticationHandler
    {
        protected override Task<IPrincipal> AuthenticateUserAsync(HttpRequestMessage request, string username, string password, System.Threading.CancellationToken cancellationToken)
        {
            if(username.Equals(password, StringComparison.InvariantCulture))
            {
                var identity = new GenericIdentity(username);
                return Task.FromResult<IPrincipal>(new GenericPrincipal(identity, null));
            }

            return Task.FromResult<IPrincipal>(null);
        }
    }
}