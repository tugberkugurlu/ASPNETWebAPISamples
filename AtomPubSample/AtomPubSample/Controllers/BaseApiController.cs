using AtomPubSample.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AtomPubSample.Controllers {
    
    public class BaseApiController : ApiController {

        protected static SafeCollection<Post> Posts = new SafeCollection<Post>();
        protected static ConcurrentDictionary<string, MediaModel> MediaItems = new ConcurrentDictionary<string, MediaModel>();
    }
}