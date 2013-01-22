using AtomPubSample.Models;
using System.Net.Http;
using System.Web.Http.Routing;

namespace AtomPubSample.Hypermedia {
    
    public class MediaResponseEnricher : IResponseEnricher {

        public bool CanEnrich(HttpResponseMessage response) {

            var content = response.Content as ObjectContent;

            return content != null
                && (content.ObjectType == typeof(MediaModel));
        }

        public HttpResponseMessage Enrich(HttpResponseMessage response) {

            MediaModel media;

            var urlHelper = response.RequestMessage.GetUrlHelper();

            if (response.TryGetContentValue<MediaModel>(out media)) {
                Enrich(media, urlHelper);
            }

            return response;
        }

        private void Enrich(MediaModel media, UrlHelper url) {

            var selfUrl = url.Link("DefaultApi", new { controller = "media", id = media.Id });
            media.AddLink(new SelfLink(selfUrl));
            media.AddLink(new EditLink(selfUrl));
        }
    }
}