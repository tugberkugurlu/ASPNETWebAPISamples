using System;
using System.Diagnostics;
using Owin;

namespace TimeoutWithOwinLimits
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                Trace.TraceInformation("OWIN Entry");
                await next();
                Trace.TraceInformation("OWIN Out");
            });
           
            // Limits Middleware...
            // ref: https://github.com/damianh/LimitsMiddleware
            app.ConnectionTimeout(TimeSpan.FromSeconds(2));
        }
    }
}