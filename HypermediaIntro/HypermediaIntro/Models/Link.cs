using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HypermediaIntro.Models {

    /// <summary>
    /// A base class for relation links
    /// </summary>
    [DataContract]
    public abstract class Link {

        [DataMember]
        public string Rel { get; private set; }

        [DataMember]
        public string Href { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        public Link(string relation, string href, string title = null) {

            Rel = relation;
            Href = href;
            Title = title;
        }
    }

    /// <summary>
    /// Refers to a resource that can be used to edit the link's context.
    /// </summary>
    public class EditLink : Link {

        public const string Relation = "edit";

        public EditLink(string href, string title = null)
            : base(Relation, href, title) {
        }
    }

    /// <summary>
    /// Conveys an identifier for the link's context.
    /// </summary>
    public class SelfLink : Link {

        private const string Relation = "self";

        public SelfLink(string href, string title = null)
            : base(Relation, href, title) {
        }
    }
}