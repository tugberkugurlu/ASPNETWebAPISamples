using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AtomPubSample.Hypermedia {

    public interface IResponseEnricher {

        bool CanEnrich(HttpResponseMessage response);
        HttpResponseMessage Enrich(HttpResponseMessage response);
    }
}