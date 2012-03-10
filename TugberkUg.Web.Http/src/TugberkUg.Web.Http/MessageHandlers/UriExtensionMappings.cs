using System.Collections.Generic;
using TugberkUg.Web.Http.Extensions;

namespace TugberkUg.Web.Http.MessageHandlers {

    public class UriExtensionMappings : List<UriExtensionMapping> {
        
        public UriExtensionMappings() {

            this.AddMapping("xml", "application/xml");
            this.AddMapping("json", "application/json");
            this.AddMapping("proto", "application/x-protobuf");
            this.AddMapping("png", "image/png");
            this.AddMapping("jpg", "image/jpg");
        }
    }
}