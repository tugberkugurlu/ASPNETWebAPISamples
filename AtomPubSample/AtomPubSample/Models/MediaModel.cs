using AtomPubSample.AtomPub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.Models {

    public class MediaModel : Resource, IPublicationMedia {

        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Summary { get; set; }
        public Uri ImageUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}