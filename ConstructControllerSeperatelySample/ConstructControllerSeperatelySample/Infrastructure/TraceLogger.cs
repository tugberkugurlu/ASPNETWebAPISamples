using System.Diagnostics;

namespace ConstructControllerSeperatelySample.Infrastructure {

    public class TraceLogger : ILogger {

        public void Log(string message) {

            Trace.TraceInformation(string.Concat("From TraceLogger: ", message));
        }
    }
}