using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace TugberkUg.Web.Http.MessageHandlers {

    public class UriExtensionMapping {

        public string Extension { get; set; }

        public MediaTypeWithQualityHeaderValue MediaType { get; set; }
    }
}
