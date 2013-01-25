using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructControllerSeperatelySample.Infrastructure {

    public interface ILogger {

        void Log(string message);
    }
}