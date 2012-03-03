using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

//Taken from System.Net.Http Library
namespace TugberkUg.Web.Http.Internal {

    internal static class FormattingUtilities {

        public const string HttpRequestedWithHeader = "x-requested-with";
        public const string HttpRequestedWithHeaderValue = "xmlhttprequest";
        public const string JsonNullLiteral = "null";
        public const string HttpHostHeader = "Host";
        public const string HttpVersionToken = "HTTP";
        public static readonly Type Utf8EncodingType = typeof(UTF8Encoding);
        public static readonly Type Utf16EncodingType = typeof(UnicodeEncoding);
        public static readonly Type HttpRequestMessageType = typeof(HttpRequestMessage);
        public static readonly Type HttpResponseMessageType = typeof(HttpResponseMessage);
        public static readonly Type HttpContentType = typeof(HttpContent);
        public static readonly Type DelegatingEnumerableGenericType = typeof(DelegatingEnumerable<>);
        public static readonly Type EnumerableInterfaceGenericType = typeof(IEnumerable<>);
        public static readonly Type QueryableInterfaceGenericType = typeof(IQueryable<>);
        public static readonly Type JsonValueType = typeof(JsonValue);
        private static readonly HashSet<string> httpContentHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {

			"Allow", 
			"Content-Disposition", 
			"Content-Encoding", 
			"Content-Language", 
			"Content-Length", 
			"Content-Location", 
			"Content-MD5", 
			"Content-Range", 
			"Content-Type", 
			"Expires", 
			"Last-Modified"
		};
        public static HashSet<string> HttpContentHeaders
        {
            get
            {
                return FormattingUtilities.httpContentHeaders;
            }
        }
        public static bool IsJsonValueType(Type type)
        {
            return FormattingUtilities.JsonValueType.IsAssignableFrom(type);
        }
        public static HttpContentHeaders CreateEmptyContentHeaders()
        {
            HttpContent httpContent = null;
            HttpContentHeaders httpContentHeaders = null;
            try
            {
                httpContent = new StringContent(string.Empty);
                httpContentHeaders = httpContent.Headers;
                httpContentHeaders.Clear();
            }
            finally
            {
                if (httpContent != null)
                {
                    httpContent.Dispose();
                }
            }
            return httpContentHeaders;
        }
        public static bool ValidateCollection(Collection<MediaTypeHeaderValue> actual, MediaTypeHeaderValue[] expected)
        {
            if (actual.Count != expected.Length)
            {
                return false;
            }
            for (int i = 0; i < expected.Length; i++)
            {
                MediaTypeHeaderValue item = expected[i];
                if (!actual.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        public static string UnquoteToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return token;
            }
            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }
            return token;
        }
    }
}