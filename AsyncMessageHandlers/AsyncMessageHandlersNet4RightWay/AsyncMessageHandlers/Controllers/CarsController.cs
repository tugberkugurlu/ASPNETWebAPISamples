using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace AsyncMessageHandlers.Controllers {
    
    public class CarsController : IHttpController {

        public Task<HttpResponseMessage> ExecuteAsync(
            HttpControllerContext controllerContext, 
            CancellationToken cancellationToken) {

            var request = controllerContext.Request;

            if (request.Method == HttpMethod.Get) {

                var cars = new[] { "Car 1", "Car 2", "Car 3" };
                var response = request.CreateResponse(HttpStatusCode.OK, cars);

                //.NET v4.0 with TaskHelpers.Sources package
                return TaskHelpers.FromResult(response);
            }

            //.NET v4.0 with TaskHelpers.Sources package
            return TaskHelpers.FromResult(
                  request.CreateResponse(HttpStatusCode.MethodNotAllowed));

        }
    }
}