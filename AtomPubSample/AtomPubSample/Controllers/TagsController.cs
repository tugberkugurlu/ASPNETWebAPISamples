using AtomPubSample.AtomPub;
using AtomPubSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.Controllers {
    
    public class TagsController : BaseApiController {

        public PublicationCategoriesDocument Get() {

            var tags = Posts.SelectMany(p => p.Tags)
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Select(t => new TagModel { Name = t, Slug = t.ToSlug() });

            var doc = new PublicationCategoriesDocument(
                Url.Link("DefaultApi", new { controller = "tags" }),
                tags,
                isFixed: false
            );

            return doc;
        }
    }
}