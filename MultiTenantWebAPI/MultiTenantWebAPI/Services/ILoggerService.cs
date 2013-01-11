using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantWebAPI.Services {
    
    public interface ILoggerService {

        void Log(string message);
    }
}