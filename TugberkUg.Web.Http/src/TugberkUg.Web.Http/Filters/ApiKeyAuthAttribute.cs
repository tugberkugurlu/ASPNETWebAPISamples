using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Common;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TugberkUg.Web.Http.Internal;

namespace TugberkUg.Web.Http.Filters {

    /// <summary>
    /// QueryString Api Key Authorization filter for ASP.NET Web API.
    /// </summary>
    public class ApiKeyAuthAttribute : AuthorizationFilterAttribute {

        private string _apiKeyQueryParameter;
        private string _roles;
        private readonly IApiKeyAuthorizer _apiKeyAuthorizer;
        private string[] _rolesSplit = AuthorizationUtilities._emptyArray;

        /// <summary>
        /// The name of the query string parameter whose value needs to be compared against.
        /// </summary>
        public string ApiKeyQueryParameter {

            get {
                return _apiKeyQueryParameter;

            } set { 

                this._apiKeyQueryParameter = value;
            }
        }

        /// <summary>
        /// The comma seperated list of roles which user needs to be in
        /// </summary>
        public string Roles {

            get {

                return this._roles ?? string.Empty;

            } set {

                this._roles = value;
                this._rolesSplit = AuthorizationUtilities.splitString(value);
            }
        }

        public ApiKeyAuthAttribute(string apiKeyQueryParameter, IApiKeyAuthorizer apiKeyAuthorizer) {

            if (string.IsNullOrEmpty(apiKeyQueryParameter))
                throw Error.ArgumentNull("apiKeyQueryParameter");

            if (apiKeyAuthorizer == null)
                throw Error.ArgumentNull("apiKeyAuthorizer");

            _apiKeyQueryParameter = apiKeyQueryParameter;
            _apiKeyAuthorizer = apiKeyAuthorizer;
        }
        public ApiKeyAuthAttribute(string apiKeyQueryParameter, string roles, IApiKeyAuthorizer apiKeyAuthorizer)
            : this(apiKeyQueryParameter, apiKeyAuthorizer) {

            _roles = roles;
        }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext) {

            if (actionContext == null)
                throw Error.ArgumentNull("actionContext");

            if (this.skipAuthorization(actionContext))
                return;

            if (!authorizeCore(actionContext.Request))
                HandleUnauthorizedRequest(actionContext);
        }

        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext) {

            if (actionContext == null) {

                throw Error.ArgumentNull("actionContext");
            }
            actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
        private bool skipAuthorization(HttpActionContext actionContext) {

	        return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>() || 
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>();
        }
        private bool authorizeCore(HttpRequestMessage request) {

            var apiKey = HttpUtility.ParseQueryString(request.RequestUri.Query)[_apiKeyQueryParameter];

            return (
                string.IsNullOrEmpty(apiKey) && 
                (_rolesSplit == AuthorizationUtilities._emptyArray) ? _apiKeyAuthorizer.IsAuthorized(apiKey) : _apiKeyAuthorizer.IsAuthorized(apiKey, _rolesSplit)
            );
        }
    }

}