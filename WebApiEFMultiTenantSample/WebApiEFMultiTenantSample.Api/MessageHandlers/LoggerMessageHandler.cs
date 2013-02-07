using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiEFMultiTenantSample.Api.Services;

namespace WebApiEFMultiTenantSample.Api.MessageHandlers {
    
    public class LoggerMessageHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            ILoggerService logger = request.GetDependencyScope().GetService(typeof(ILoggerService)) as ILoggerService;
            logger.Log("LoggerMessageHandler.SendAsync");
            return base.SendAsync(request, cancellationToken);
        }
    }
}