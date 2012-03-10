using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TugberkUg.Web.Http.Formatting {

    public class JsonNetFormatter : MediaTypeFormatter {

        private JsonSerializerSettings _jsonSerializerSettings;

        public JsonNetFormatter(JsonSerializerSettings jsonSerializerSettings) {

            _jsonSerializerSettings = jsonSerializerSettings ?? new JsonSerializerSettings();

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            Encoding = new UTF8Encoding(false, true);
        }

        protected override bool CanReadType(Type type) {

            return
                (type == typeof(IKeyValueModel)) ? false : true;
        }

        protected override bool CanWriteType(Type type) {

            return true;
        }

        protected override Task<object> OnReadFromStreamAsync(
            Type type, 
            Stream stream, 
            HttpContentHeaders contentHeaders, 
            FormatterContext formatterContext) {

                JsonSerializer serializer = JsonSerializer.Create(_jsonSerializerSettings);

                return Task.Factory.StartNew(() => {

                    using (StreamReader streamReader = new StreamReader(stream))
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                        return
                            serializer.Deserialize(jsonTextReader, type);
                });
        }

        protected override Task OnWriteToStreamAsync(
            Type type, 
            object value, 
            Stream stream, 
            HttpContentHeaders contentHeaders, 
            FormatterContext formatterContext, 
            System.Net.TransportContext transportContext) {

                JsonSerializer serializer = JsonSerializer.Create(_jsonSerializerSettings);

                return Task.Factory.StartNew(() => {

                    using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(stream, Encoding)) { CloseOutput = false }) {
                    
                        serializer.Serialize(jsonTextWriter, value);
                        jsonTextWriter.Flush();
                    }
                });
        }
    }
}