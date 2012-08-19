using System;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebAPITracingNugetPackage
{
    // TODO: To enable tracing in your application, please add the following line of code
    // to your startup code (WebApiConfig.cs or Global.asax.cs in an MVC 4 project):
    //      TraceConfig.Register(config);
    // For more information, refer to: http://www.asp.net/web-api

    /// <summary>
    /// This static class contains helper methods related to the registration
    /// of <see cref="ITraceWriter"/> instances.
    /// </summary>
    public static class TraceConfig
    {
        /// <summary>
        /// Registers the <see cref="ITraceWriter"/> implementation to use
        /// for this application.
        /// </summary>
        /// <param name="configuration">The <see cref="HttpConfiguration"/> in which
        /// to register the trace writer.</param>
        public static void Register(HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            SystemDiagnosticsTraceWriter traceWriter =
                new SystemDiagnosticsTraceWriter()
                {
                    MinimumLevel = TraceLevel.Info,
                    IsVerbose = true
                };

            configuration.Services.Replace(typeof(ITraceWriter), traceWriter);
        }
    }
}
