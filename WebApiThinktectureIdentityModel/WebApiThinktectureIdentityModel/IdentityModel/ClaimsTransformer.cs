using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApiThinktectureIdentityModel.IdentityModel {

    public class ClaimsTransformer : ClaimsAuthenticationManager {

        public override ClaimsPrincipal Authenticate(
            string resourceName, ClaimsPrincipal incomingPrincipal) {

            if (!incomingPrincipal.Identity.IsAuthenticated) {

                return base.Authenticate(resourceName, incomingPrincipal);
            }

            incomingPrincipal.Identities.First().AddClaim(new Claim("localClaim", "someValue"));
            return incomingPrincipal;
        }
    }
}