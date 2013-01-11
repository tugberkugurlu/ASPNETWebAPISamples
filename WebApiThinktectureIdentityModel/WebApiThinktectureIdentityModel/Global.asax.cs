using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
﻿using System.IdentityModel.Selectors;
using Thinktecture.IdentityModel.Tokens.Http;
using WebApiThinktectureIdentityModel.IdentityModel;
using System.Web.Routing;

namespace WebApiThinktectureIdentityModel {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            RegisterAuth(config);

            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }

        private void RegisterAuth(HttpConfiguration config) {

            // NOTE: You need to get into the ASP.NET Web API pipeline
            // in order to retrieve the session token.
            // e.g: GET /token should get you the token but instead you get 404.
            // but GET /api/token works as you are inside the ASP.NET Web API pipeline now.

            var auth = new AuthenticationConfiguration { 
                // ClaimsAuthenticationManager = new ClaimsTransformer(),
                DefaultAuthenticationScheme = "Basic",
                EnableSessionToken = true // default lifetime is 10 hours
            };

            auth.AddBasicAuthentication(IsValid);
            var authHandler = new AuthenticationHandler(auth);
            config.MessageHandlers.Add(authHandler);
        }

        private bool IsValid(string username, string password) {

            // dHVnYmVyazp0dWdiZXJr
            bool isValidLogin = username.Equals(
                password, StringComparison.InvariantCultureIgnoreCase);

            return isValidLogin;
        }
    }
}