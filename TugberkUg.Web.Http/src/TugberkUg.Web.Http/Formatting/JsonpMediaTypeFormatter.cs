using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

//https://github.com/ChristianWeyer/Thinktecture.Web.Http/blob/master/Thinktecture.Web.Http/Formatters/JsonpFormatter.cs
namespace TugberkUg.Web.Http.Formatting {

    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter {

        private string callbackQueryParameter;

        public JsonpMediaTypeFormatter() {

            SupportedMediaTypes.Add(DefaultMediaType);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            MediaTypeMappings.Add(
                new UriPathExtensionMapping("jsonp", DefaultMediaType)
            );
        }

        public string CallbackQueryParameter {

            get { return callbackQueryParameter ?? "callback"; }
            set { callbackQueryParameter = value; }
        }

        protected override bool CanWriteType(Type type)
        {
            return true;
        }

        protected override bool CanReadType(Type type)
        {
            return true;
        }

        protected override Task OnWriteToStreamAsync(
            Type type, 
            object value, 
            System.IO.Stream stream, 
            HttpContentHeaders contentHeaders, 
            FormatterContext formatterContext, 
            TransportContext transportContext) {

            string callback;

            if (IsJsonpRequest(formatterContext.Response.RequestMessage, out callback)) {

                return Task.Factory.StartNew(() => {

                    var writer = new StreamWriter(stream);
                    writer.Write(string.Format("callback{0}", "("));
                    writer.Flush();
                    base.OnWriteToStreamAsync(
                        type, value, stream,
                        contentHeaders, formatterContext, transportContext).Wait();

                    writer.Write(")");
                    writer.Flush();

                });

            } else {

                return base.OnWriteToStreamAsync(type, value, stream, contentHeaders, formatterContext, transportContext);
            }
        }

        private bool IsJsonpRequest(HttpRequestMessage request, out string callback) {

            callback = null;

            if (request.Method != HttpMethod.Get) {

                return false;
            }

            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            callback = query[CallbackQueryParameter];

            return 
                !string.IsNullOrEmpty(callback);
        }
    }
}