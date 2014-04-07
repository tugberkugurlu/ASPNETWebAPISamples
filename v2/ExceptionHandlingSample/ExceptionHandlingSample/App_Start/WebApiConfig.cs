using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace ExceptionHandlingSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionHandler), new MyGlobalHandler());
        }
    }

    public class MyGlobalHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            const string Message = "Your input conflicted with the current state of the system.";
            if (context.Exception is DivideByZeroException)
            {
                context.Result = new ResponseMessageResult(context.Request.CreateErrorResponse(HttpStatusCode.Conflict, Message));
            }
            else
            {
                HttpError error = new HttpError("An interval server error occured.");
                error.Add("CorrelationId", context.Request.GetCorrelationId().ToString());
                var response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
                context.Result = new ResponseMessageResult(response);
            }
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
}
