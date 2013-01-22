using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebAPIDoodle.Http;

namespace AtomPubSample.MessageHandlers {
    
    public class BasicAuthHandler : BasicAuthenticationHandler {

        protected override Task<IPrincipal> AuthenticateUserAsync(HttpRequestMessage request, 
            string username, string password, CancellationToken cancellationToken) {

            if (username.Equals(password, StringComparison.InvariantCultureIgnoreCase)) {

                IPrincipal principal = new GenericPrincipal(new GenericIdentity(username), null);
                return Task.FromResult(principal);
            }

            return Task.FromResult<IPrincipal>(null);
        }
    }
}