using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using TugberkUg.Web.Http.MessageHandlers;

namespace TugberkUg.Web.Http.Extensions {

    public static class UriExtensionMappingExtensions {

        public static void AddMapping(this IList<UriExtensionMapping> mappings, string extension, string mediaType) {

            mappings.Add(new UriExtensionMapping { Extension = extension, MediaType = new MediaTypeWithQualityHeaderValue(mediaType) });
        }
    }
}