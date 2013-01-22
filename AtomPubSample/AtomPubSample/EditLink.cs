using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtomPubSample {

    public class EditLink : Link {
        public const string Relation = "edit";

        public EditLink(string href, string title = null)
            : base(Relation, href, title) {
        }
    }
}