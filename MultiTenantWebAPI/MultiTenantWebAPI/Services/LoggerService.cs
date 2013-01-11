using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MultiTenantWebAPI.Services {
    
    public class LoggerService : ILoggerService {

        public LoggerService() {
        }

        public void Log(string message) {

            Trace.TraceInformation(message);
        }
    }
}