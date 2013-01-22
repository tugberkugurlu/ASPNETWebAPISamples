using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample.AtomPub {

    public class PublicationCategoriesDocument : IPublicationCategoriesDocument {

        public string Scheme { get; set; }
        public IEnumerable<IPublicationCategory> Categories { get; set; }
        public bool IsFixed { get; set; }

        public PublicationCategoriesDocument(string scheme, IEnumerable<IPublicationCategory> categories, bool isFixed = false) {

            Scheme = scheme;
            Categories = categories;
            IsFixed = isFixed;
        }
    }
}