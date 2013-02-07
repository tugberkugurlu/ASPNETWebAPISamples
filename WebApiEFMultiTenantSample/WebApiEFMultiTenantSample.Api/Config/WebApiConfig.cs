using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiEFMultiTenantSample.Api.MessageHandlers;

namespace WebApiEFMultiTenantSample.Api.Config {
    
    public static class WebApiConfig {

        public static void Configure(HttpConfiguration config) {

            config.MessageHandlers.Add(new LoggerMessageHandler());
        }
    }
}