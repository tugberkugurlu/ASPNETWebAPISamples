using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DependencyScopeTracingDisposeBug.Models.Entities {

    public class WebApiTracerContext : EntitiesContext {

        public IDbSet<HttpApiLogRecord> HttpApiLogRecords { get; set; }
    }
}