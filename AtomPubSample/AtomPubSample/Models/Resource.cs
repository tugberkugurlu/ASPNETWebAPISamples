using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.Models {

    public class Resource {

        private readonly List<Link> links;

        public IEnumerable<Link> Links { get { return links; } }

        public Resource() {

            links = new List<Link>();
        }

        public void AddLink(Link link) {
            
            links.Add(link);
        }
    }
}