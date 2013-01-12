using DependencyScopeTracingDisposeBug.Models.Entities;
using DependencyScopeTracingDisposeBug.Services;
using System;
using System.Net.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Tracing;

namespace DependencyScopeTracingDisposeBug.Tracing {

    public class WebApiTracer : ITraceWriter {

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction) {

            if (level != TraceLevel.Off) {

                TraceRecord record = new TraceRecord(request, category, level);
                traceAction(record);
                Log(record);
            }
        }

        private void Log(TraceRecord traceRecord) {

            IDependencyScope dependencyScope = traceRecord.Request.GetDependencyScope();
            ILoggerService loggerService = dependencyScope.GetService(typeof(ILoggerService)) as ILoggerService;

            loggerService.Log(new HttpApiLogRecord {
                CorrelationId = traceRecord.RequestId,
                RequestUri = traceRecord.Request.RequestUri.ToString(),
                IpAddress = traceRecord.Request.GetUserHostAddress(),
                HttpMethod = traceRecord.Request.Method.ToString(),
                UserAgent = traceRecord.Request.Headers.UserAgent.ToString(),
                Category = traceRecord.Category,
                Level = traceRecord.Level.ToString(),
                Kind = traceRecord.Kind.ToString(),
                Operator = traceRecord.Operator,
                Operation = traceRecord.Operation,
                ResponseStatusCode = traceRecord.Status.GetHashCode(),
                LogMessage = traceRecord.Message,
                ExceptionType = traceRecord.Exception != null ? traceRecord.Exception.GetType().ToString() : null,
                BaseExceptionType = traceRecord.Exception != null ? traceRecord.Exception.GetBaseException().GetType().ToString() : null,
                ExceptionMessage = traceRecord.Exception != null ? traceRecord.Exception.Message : null,
                ExceptionStackTrace = traceRecord.Exception != null ? traceRecord.Exception.StackTrace : null,
                Timestamp = traceRecord.Timestamp
            });
        }
    }
}