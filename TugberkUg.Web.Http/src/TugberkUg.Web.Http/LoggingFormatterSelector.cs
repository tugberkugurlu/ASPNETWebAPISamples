using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace TugberkUg.Web.Http {

    //reference code
    //ref: https://gist.github.com/1881457
    internal class LoggingFormatterSelector : IFormatterSelector {

        private readonly FormatterSelector _FormatterSelector = new FormatterSelector();

        public MediaTypeFormatter SelectReadFormatter(Type type, FormatterContext formatterContext, IEnumerable<MediaTypeFormatter> formatters)
        {
            string descriptFormatterContext = GetDescriptFormatterContext(formatterContext);

            Console.WriteLine("Selecting Read Formatter for type {0} based on formatter context: {1}", type.Name, descriptFormatterContext);
            var mediaTypeFormatter = _FormatterSelector.SelectReadFormatter(type, formatterContext, formatters);
            Console.WriteLine("Selected Read Formatter : {0}", mediaTypeFormatter.GetType().Name);
            return mediaTypeFormatter;
        }


        public MediaTypeFormatter SelectWriteFormatter(Type type, FormatterContext formatterContext, IEnumerable<MediaTypeFormatter> formatters, out MediaTypeHeaderValue mediaType)
        {
            string descriptFormatterContext = GetDescriptFormatterContext(formatterContext);

            Console.WriteLine("Selecting Write Formatter for type {0} based on formatter context: {1}", type.Name, descriptFormatterContext);
            var mediaTypeFormatter = _FormatterSelector.SelectWriteFormatter(type, formatterContext, formatters, out mediaType);
            Console.WriteLine("Selected Write Formatter : {0} {1}", mediaTypeFormatter.GetType().Name, mediaType);
            return mediaTypeFormatter;
        }


        private string GetDescriptFormatterContext(FormatterContext formatterContext)
        {
            return String.Format("ContentType {0}, HasRequest {1}, HasResponse {2}, IsRead {3} ",
                            formatterContext.ContentType , 
                            formatterContext.Request != null,
                            formatterContext.Response != null, 
                            formatterContext.IsRead);
        }

    }
}

//Which can be plugged in like this,

//    config.ServiceResolver.SetService(typeof(IFormatterSelector),new LoggingFormatterSelector());