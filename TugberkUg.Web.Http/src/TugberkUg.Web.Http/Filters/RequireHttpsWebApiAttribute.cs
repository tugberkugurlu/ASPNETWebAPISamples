using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TugberkUg.Web.Http.Filters {

    //http://blogs.msdn.com/b/carlosfigueira/archive/2012/03/09/implementing-requirehttps-with-asp-net-web-api.aspx
    public class RequireHttpsWebApiAttribute : AuthorizationFilterAttribute {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext) {

            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps) {

                HttpResponseMessage response;

                if (request.Method.Equals(HttpMethod.Get)) {

                    response = request.CreateResponse(HttpStatusCode.Found);
                    UriBuilder uri = new UriBuilder(request.RequestUri);
                    uri.Scheme = Uri.UriSchemeHttps;
                    uri.Port = 443;
                    response.Headers.Location = uri.Uri;

                } else {

                    response = request.CreateResponse(HttpStatusCode.InternalServerError);
                    response.Content = new StringContent("This request needs to be made via HTTPS.");
                }

                actionContext.Response = response;
            }
        }

        private void HandleNonHttpsRequest(HttpActionContext actionContext) {

            // redirect to HTTPS version of page
            string url = "https://" + actionContext.Request.RequestUri.Host + actionContext.Request.RequestUri.PathAndQuery;

            actionContext.Response.Headers.Location = new Uri(url);
            actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Redirect);
        }
    }
}