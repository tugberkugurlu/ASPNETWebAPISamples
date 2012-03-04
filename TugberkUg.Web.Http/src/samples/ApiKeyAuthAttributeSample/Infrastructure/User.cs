using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiKeyAuthAttributeSample.Infrastructure {

    public class User {

        public string ApiKey { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}