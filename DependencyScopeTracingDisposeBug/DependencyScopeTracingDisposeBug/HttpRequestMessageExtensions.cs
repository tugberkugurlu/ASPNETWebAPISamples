using DependencyScopeTracingDisposeBug.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DependencyScopeTracingDisposeBug {

    internal static class HttpRequestMessageExtensions {

        internal static ILoggerService GetLoggingService(this HttpRequestMessage request) {

            if (request == null) {

                throw new ArgumentNullException("request");
            }

            return request.GetService<ILoggerService>();
        }

        internal static HttpContextBase GetHttpContext(this HttpRequestMessage request) {

            return request.GetProperty<HttpContextBase>(ApiCommonRequestKeys.MS_HttpContextKey);
        }

        internal static string GetUserHostAddress(this HttpRequestMessage request) {

            return request.GetHttpContext().Request.UserHostAddress;
        }

        internal static TService GetService<TService>(this HttpRequestMessage request) {

            var dependencyScope = request.GetDependencyScope();
            var service = (TService)dependencyScope.GetService(typeof(TService));

            return service;
        }

        internal static T GetProperty<T>(this HttpRequestMessage request, string key) {

            T value;
            request.Properties.TryGetValue(key, out value);
            return value;
        }
    }
}