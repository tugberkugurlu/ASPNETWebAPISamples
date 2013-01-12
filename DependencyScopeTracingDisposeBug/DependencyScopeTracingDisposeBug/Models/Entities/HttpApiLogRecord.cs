using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyScopeTracingDisposeBug.Models.Entities {

    public class HttpApiLogRecord : IEntity {

        public int Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string UserId { get; set; }
        public string RequestUri { get; set; }
        public string IpAddress { get; set; }
        public string HttpMethod { get; set; }
        public string UserAgent { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string Kind { get; set; }
        public string Operator { get; set; }
        public string Operation { get; set; }
        public Nullable<int> ResponseStatusCode { get; set; }
        public string LogMessage { get; set; }
        public string ExceptionType { get; set; }
        public string BaseExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime Timestamp { get; set; }
    }
}