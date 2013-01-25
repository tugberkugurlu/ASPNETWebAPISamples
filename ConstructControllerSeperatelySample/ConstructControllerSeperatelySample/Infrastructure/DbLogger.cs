using System.Diagnostics;

namespace ConstructControllerSeperatelySample.Infrastructure {

    public class DbLogger : ILogger {

        public void Log(string message) {

            Trace.TraceInformation(string.Concat("From DbLogger: ", message));
        }
    }
}