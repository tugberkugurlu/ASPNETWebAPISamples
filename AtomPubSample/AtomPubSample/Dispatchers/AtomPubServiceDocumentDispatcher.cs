using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using System.Xml;

namespace AtomPubSample.Dispatchers {
    
    public class AtomPubServiceDocumentDispatcher : HttpMessageHandler {

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken) {

            UrlHelper url = request.GetUrlHelper();
            ServiceDocument doc = new ServiceDocument();
            Workspace ws = new Workspace() {

                Title = new TextSyndicationContent("My Site"),
                BaseUri = new Uri(request.RequestUri.GetLeftPart(UriPartial.Authority))
            };

            ws.Collections.Add(GetPostsResourceCollectionInfo(url));
            ws.Collections.Add(GetMediaResourceCollectionInfo(url));
            doc.Workspaces.Add(ws);

            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
            var formatter = new AtomPub10ServiceDocumentFormatter(doc);

            var stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream)) {
                formatter.WriteTo(writer);
            }

            stream.Position = 0;
            var content = new StreamContent(stream);
            response.Content = content;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/atomsvc+xml");

            return Task.FromResult(response);
        }

        private ResourceCollectionInfo GetPostsResourceCollectionInfo(UrlHelper url) {

            ResourceCollectionInfo posts = new ResourceCollectionInfo("Blog",
                new Uri(url.Link("DefaultApi", new { controller = "posts" })));

            posts.Accepts.Add("application/atom+xml;type=entry");

            // For WLW to work we need to include format in the categories URI.
            // Hoping to provide a better solution than this.
            var categoriesUri = new Uri(url.Link("DefaultApi", new { controller = "tags", format = "atomcat" }));
            ReferencedCategoriesDocument categories = new ReferencedCategoriesDocument(categoriesUri);
            posts.Categories.Add(categories);

            return posts;
        }

        // http://atompub-mulitpart-spec.googlecode.com/svn/trunk/draft-gregorio-atompub-multipart-03.html
        // https://tools.ietf.org/html/rfc5023#section-9.6
        // http://msdn.microsoft.com/en-us/magazine/dd569753.aspx
        private ResourceCollectionInfo GetMediaResourceCollectionInfo(UrlHelper url) {

            ResourceCollectionInfo pics = new ResourceCollectionInfo("Pictures",
                new Uri(url.Link("DefaultApi", new { controller = "media" })));

            pics.Accepts.Add("image/png");
            pics.Accepts.Add("image/jpeg");
            pics.Accepts.Add("image/gif");

            return pics;
        }
    }
}