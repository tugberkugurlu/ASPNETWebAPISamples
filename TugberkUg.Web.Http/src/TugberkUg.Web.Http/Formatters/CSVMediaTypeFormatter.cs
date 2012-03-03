using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TugberkUg.Web.Http.Formatters {

    public class CSVMediaTypeFormatter : MediaTypeFormatter {

        public CSVMediaTypeFormatter() {

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }
        public CSVMediaTypeFormatter(MediaTypeMapping mediaTypeMapping) : this() {

            //MediaTypeMappings.Add(new RouteDataMediaTypeMapping("extension", "csv", new MediaTypeHeaderValue("text/csv")));
            MediaTypeMappings.Add(mediaTypeMapping);
        }
        public CSVMediaTypeFormatter(IEnumerable<MediaTypeMapping> mediaTypeMappings) : this() {

            foreach (var mediaTypeMapping in mediaTypeMappings) {
                MediaTypeMappings.Add(mediaTypeMapping);
            }
        }

        protected override bool CanWriteType(Type type) {

            if (type == null)
                throw new ArgumentNullException("type");

            return true;
        }

        protected override Task OnWriteToStreamAsync(
            Type type,
            object value,
            Stream stream,
            HttpContentHeaders contentHeaders,
            FormatterContext formatterContext,
            TransportContext transportContext) {

            return Task.Factory.StartNew(() => {
                writeStream(type, value, stream, contentHeaders);
            });
        }

        //private utils
        private void writeStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders) {

            if (isTypeOfIEnumerable(type)) {

                Type itemType = type.GetGenericArguments()[0];

                using (StringWriter _stringWriter = new StringWriter()) {

                    _stringWriter.WriteLine(
                        string.Join<string>(
                            ",", itemType.GetProperties().Select(x => x.Name )
                        )
                    );

                    foreach (var obj in (IEnumerable<object>)value) {

                        var vals = obj.GetType().GetProperties().Select(
                            pi => new { 
                                Value = pi.GetValue(obj, null)
                            }
                        );

                        string _valueLine = string.Empty;

                        foreach (var val in vals) {

                            if (val.Value != null) {

                                var _val = val.Value.ToString();

                                //Check if the value contans a comma and place it in quotes if so
                                if (_val.Contains(","))
                                    _val = string.Concat("\"", _val, "\"");

                                //Replace any \r or \n special characters from a new line with a space
                                if (_val.Contains("\r"))
                                    _val = _val.Replace("\r", " ");
                                if (_val.Contains("\n"))
                                    _val = _val.Replace("\n", " ");

                                _valueLine = string.Concat(_valueLine, _val, ",");

                            } else {

                                _valueLine = string.Concat(string.Empty, ",");
                            }
                        }

                        _stringWriter.WriteLine(_valueLine.TrimEnd(','));
                    }

                    using (var streamWriter = new StreamWriter(stream))
                        streamWriter.Write(_stringWriter.ToString());
                }
            }
        }
        private bool isTypeOfIEnumerable(Type type) {

            foreach (Type interfaceType in type.GetInterfaces()) {

                if (interfaceType.IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    return true;
            }

            return false;
        }

    }
}