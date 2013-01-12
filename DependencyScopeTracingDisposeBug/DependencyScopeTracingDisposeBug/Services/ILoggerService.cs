using DependencyScopeTracingDisposeBug.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyScopeTracingDisposeBug.Services {
    
    public interface ILoggerService {

        void Log(HttpApiLogRecord httpApiLogRecord);
    }
}