using System;
using System.Collections.Generic;

namespace AtomPubSample.AtomPub {
    
    public interface IPublicationMedia {

        string Id { get; }
        string Title { get; }
        string AuthorName { get; }
        string Summary { get; }
        Uri ImageUrl { get; }
        string ContentType { get;}
        DateTime LastUpdated { get; }
        IEnumerable<Link> Links { get; }
    }
}