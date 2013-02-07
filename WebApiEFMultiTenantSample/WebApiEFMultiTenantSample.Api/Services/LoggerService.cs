using System.Diagnostics;

namespace WebApiEFMultiTenantSample.Api.Services {

    public class LoggerService : ILoggerService {

        private readonly string _tenant;
        public LoggerService(string tenant) {

            _tenant = tenant;
        }

        public void Log(string message) {

            Trace.TraceInformation("Log from tenant ({0}): {1}", _tenant, message);
        }
    }
}