using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TugberkUg.Web.Http.Extensions;

namespace TugberkUg.Web.Http.MessageHandlers {

    //https://github.com/thinktecture/Thinktecture.Web.Http/blob/master/Thinktecture.Web.Http/Handlers/UriFormatExtensionHandler.cs
    public class UriFormatExtensionHandler : DelegatingHandler {

        private static readonly Dictionary<string, MediaTypeWithQualityHeaderValue> extensionMappings = new Dictionary<string, MediaTypeWithQualityHeaderValue>();

        public UriFormatExtensionHandler(IEnumerable<UriExtensionMapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                extensionMappings[mapping.Extension] = mapping.MediaType;
            }
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var segments = request.RequestUri.Segments;
            var lastSegment = segments.LastOrDefault();
            MediaTypeWithQualityHeaderValue mediaType;
            var found = extensionMappings.TryGetValue(lastSegment, out mediaType);
            
            if (found)
            {
                var newUri = request.RequestUri.OriginalString.Replace("/" + lastSegment, "");
                request.RequestUri = new Uri(newUri, UriKind.Absolute);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(mediaType);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
