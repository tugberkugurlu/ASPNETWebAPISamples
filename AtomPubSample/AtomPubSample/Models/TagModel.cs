using AtomPubSample.AtomPub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.Models {

    public class TagModel : IPublicationCategory {

        public string Name { get; set; }
        public string Slug { get; set; }

        string IPublicationCategory.Label {

            get { return Name; }
        }
    }
}