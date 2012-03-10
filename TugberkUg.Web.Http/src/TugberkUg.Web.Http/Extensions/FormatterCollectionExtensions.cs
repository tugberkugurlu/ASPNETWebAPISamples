using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace TugberkUg.Web.Http.Extensions {

    //reference code
    //ref: https://gist.github.com/1654104
    internal static class FormatterCollectionExtensions {

        public static Tuple<MediaTypeFormatter, MediaTypeHeaderValue> Negotiate<T>(
            this MediaTypeFormatterCollection formatters,
            HttpRequestMessage requestMessage) {

                var formatSelector = new FormatterSelector();
                MediaTypeHeaderValue mediaType = null;
                var response = new HttpResponseMessage() { 
                    RequestMessage = requestMessage
                };

                var formatter = formatSelector.SelectWriteFormatter(
                    typeof(T),
                    new FormatterContext(response, false),
                    formatters,
                    out mediaType);

                return
                    new Tuple<MediaTypeFormatter, MediaTypeHeaderValue>(
                        formatter, mediaType
                    );
            
            /*
             Usage:
             public HttpResponseMessage Get() {
             
                var response = new HttpResponseMessage();

                var contact = new Contact() {FirstName = "Joe", LastName = "Brown"};
            
                var mediaInfo = Configuration.Formatters.Negotiate<Contact>(Request);
            
                response.Content = new SimpleObjectContent<Contact>(contact, mediaInfo.Item1);
                response.Content.Headers.ContentType = mediaInfo.Item2;

                return response;
            }
             */
        }
    }
}