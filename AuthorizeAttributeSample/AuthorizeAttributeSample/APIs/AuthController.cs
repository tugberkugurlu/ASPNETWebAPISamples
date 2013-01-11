using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Security;
using System.Net;
using AuthorizeAttributeSample.Models;
using AuthorizeAttributeSample.Services;

namespace AuthorizeAttributeSample.APIs {

    public class AuthController : ApiController {

        private readonly AuthorizationService authService = 
            new AuthorizationService();

        private readonly FormsAuthenticationService formsAuthService = 
            new FormsAuthenticationService();

        [AllowAnonymous]
        public HttpResponseMessage Post(User user) {

            var response = new HttpResponseMessage();
            var authResult = authService.Authorize(user.UserName, user.Password);

            if (user != null && authResult.Item1) {

                //user has been authorized
                response.StatusCode = HttpStatusCode.OK;
                formsAuthService.SignIn(
                    user.UserName, 
                    true,
                    authResult.Item2.Roles.Select(x => x.Name).ToArray());

                return response;
            }

            //if we come this far, it means that user hasn't been authorized
            response.StatusCode = HttpStatusCode.Unauthorized;

            return response;
        }
    }
}