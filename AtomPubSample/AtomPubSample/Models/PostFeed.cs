using AtomPubSample.AtomPub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.Models {

    public class PostFeed : Resource, IPublicationFeed {

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public IEnumerable<PostModel> Posts { get; set; }

        IEnumerable<IPublication> IPublicationFeed.Items {

            get { return Posts; }
        }
    }
}