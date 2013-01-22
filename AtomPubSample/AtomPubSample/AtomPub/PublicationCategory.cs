using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.AtomPub {

    public class PublicationCategory : IPublicationCategory {

        public string Name { get; set; }
        public string Label { get; set; }

        public PublicationCategory(string name, string label = null) {
            
            Name = name;
            Label = label;
        }
    }
}