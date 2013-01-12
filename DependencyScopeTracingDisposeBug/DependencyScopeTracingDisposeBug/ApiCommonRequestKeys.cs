using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyScopeTracingDisposeBug {

    internal static class ApiCommonRequestKeys {

        internal const string MS_HttpContextKey = "MS_HttpContext";
        internal const string MS_UserHostAddressKey = "MS_UserHostAddress";
    }
}