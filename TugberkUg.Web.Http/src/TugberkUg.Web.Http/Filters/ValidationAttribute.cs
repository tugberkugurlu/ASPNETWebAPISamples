using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TugberkUg.Web.Http.Filters {

    public class ValidationAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(HttpActionContext actionContext) {

            var modelState = actionContext.ModelState;

            if (!modelState.IsValid) {

                dynamic errors = new JsonObject();

                foreach (var key in modelState.Keys) {

                    var state = modelState[key];

                    if (state.Errors.Any())
                        errors[key] = state.Errors.First().ErrorMessage;
                }

                actionContext.Response = new HttpResponseMessage<JsonValue>(errors, HttpStatusCode.BadRequest);
            }
        }
    }
}