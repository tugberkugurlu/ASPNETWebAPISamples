using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TugberkUg.Web.Http.Formatting {

    public class PlainTextFormatter : MediaTypeFormatter {

        public PlainTextFormatter() {

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        protected override System.Threading.Tasks.Task<object> OnReadFromStreamAsync(
            Type type, 
            System.IO.Stream stream, 
            HttpContentHeaders contentHeaders, 
            FormatterContext formatterContext) {

                return
                    new Task<object>(() => {
                        return new StreamReader(stream).ReadToEnd();
                    });
        }

        protected override Task OnWriteToStreamAsync(
            Type type, 
            object value, 
            Stream stream, 
            HttpContentHeaders contentHeaders, 
            FormatterContext formatterContext, 
            System.Net.TransportContext transportContext) {

            return
                new Task(() => {

                    var writer = new StreamWriter(stream);
                    writer.Write(value.ToString());
                });
        }

        protected override bool CanWriteType(Type type) {

            return true;
        }

        protected override bool CanReadType(Type type) {

            return true;
        }
    }
}
