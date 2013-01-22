using AtomPubSample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace AtomPubSample.Controllers {
    
    public class MediaController : BaseApiController {

        private static readonly ReadOnlyDictionary<string, string> MimeTypes = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) { 
                { "image/png", ".png" },
                { "image/jpeg", ".jpg" },
                { "image/gif", ".gif" }
            }
        );

        public MediaModel GetMedia(string id) {

            MediaModel media;
            if (!MediaItems.TryGetValue(id, out media)) {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return media;
        }

        public MediaModel PutMedia(string id) {

            MediaModel media;
            if (!MediaItems.TryGetValue(id, out media)) {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // TODO: Update the image here...

            return media;
        }

        public async Task<HttpResponseMessage> PostMedia() { 

            //// Check if there is anything inside the message body
            if (Request.Content != null && Request.Content.Headers.ContentLength > 0) {

                // TODO: Try to solve the retrieval problem with a custom
                // Parameter Binding impl.

                // GIANT_NOTE: Don't do this like below at home.
                HttpContextBase httpContext = Request.Properties["MS_HttpContext"] as HttpContextBase;
                Guid id = Guid.NewGuid();
                string contentType = Request.Content.Headers.ContentType.MediaType;
                string extension = GetExtension(contentType);
                string path = "app_files";
                string appPath = HostingEnvironment.ApplicationPhysicalPath;
                string dirPath = string.Concat(appPath, "\\", path);
                string fileName = string.Concat(id.ToString(), extension);
                string fullFileName = string.Concat(dirPath, "\\", fileName);
                Uri selfLink = new Uri(Url.Link("DefaultApi", new { controller = "media", id = id }));
                Uri contentLink = new Uri(string.Concat(Request.RequestUri.GetLeftPart(UriPartial.Authority), "/", path, "/", fileName));

                if(!Directory.Exists(dirPath)) { 
                    Directory.CreateDirectory(dirPath);
                }

                using(Stream contentStream = await Request.Content.ReadAsStreamAsync())
                using(FileStream fileStream = File.Create(fullFileName)) {
                    contentStream.Seek(0, SeekOrigin.Begin);
                    await contentStream.CopyToAsync(fileStream);
	            }

                var mediaModel = new MediaModel { 
                    Id = id.ToString(),
                    // AuthorName = "Tugberk",
                    // Title = "Awesome Pic Title...",
                    // Summary = "Awesome Pic Summary...",
                    ImageUrl = contentLink,
                    ContentType = contentType,
                    LastUpdated = DateTime.UtcNow
                };

                MediaItems.TryAdd(id.ToString(), mediaModel);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, mediaModel);
                response.Headers.Location = selfLink;
                return response;
            }

            return Request.CreateResponse(HttpStatusCode.Conflict);
        }

        private string GetExtension(string mimeType) {

            return MimeTypes.FirstOrDefault(x => x.Key.Equals(mimeType)).Value;
        }
    }
}