using AtomPubSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AtomPubSample.Controllers {

    public class PostsController : BaseApiController {

        // GET api/posts
        public PostFeed Get() {

            var feed = new PostFeed {
                Title = "My Post Feed",
                Author = "John Doe",
                Summary = "A blog about Atom-Powered robots.",
                Posts = Posts.Select(p =>
                    new PostModel(p, GetCategoryScheme())).OrderByDescending(p => p.PublishDate).ToArray()
            };

            return feed;
        }

        // GET api/posts/5
        public PostModel Get(int id) {
            return new PostModel(GetPost(id), GetCategoryScheme());
        }

        // POST api/posts
        public HttpResponseMessage Post(AddPostCommand command) {

            var postContent = Request.Content.ReadAsStringAsync().Result;

            var post = new Post {
                Id = GetNextId(),
                Title = command.Title,
                Slug = command.Slug ?? command.Title.ToSlug(),
                Summary = command.Summary,
                ContentType = command.ContentType,
                Content = command.Content,
                Tags = command.Tags,
                PublishDate = command.PublishDate ?? DateTime.UtcNow
            };

            Posts.Add(post);

            var response = Request.CreateResponse(HttpStatusCode.Created, new PostModel(post, GetCategoryScheme()));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { controller = "posts", id = post.Id }));

            return response;
        }

        // PUT api/posts/5
        public HttpResponseMessage Put(int id, UpdatePostCommand command) {

            var post = GetPost(id);

            post.Title = command.Title;
            post.Slug = command.Slug ?? post.Slug;
            post.Summary = command.Summary;
            post.ContentType = command.ContentType;
            post.Content = command.Content;
            post.Tags = command.Tags;
            post.PublishDate = command.PublishDate ?? post.PublishDate;

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE api/posts/5
        public HttpResponseMessage Delete(int id) {

            var post = GetPost(id);
            Posts.Remove(post);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private Post GetPost(int id) {

            var post = Posts.SingleOrDefault(p => p.Id == id);

            if (post == null) {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return post;
        }

        private int GetNextId() {
            return Posts.Count > 0 ? Posts.Max(p => p.Id) + 1 : 1;
        }

        private string GetCategoryScheme() {
            return Url.Link("DefaultApi", new { controller = "tags" });
        }
    }
}