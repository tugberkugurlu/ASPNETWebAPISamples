using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugberkUg.Web.Http {

    public class ApiKeyAuthorizer : IApiKeyAuthorizer {

        public bool IsAuthorized(string apiKey) {

            throw new NotImplementedException();
        }
        public bool IsAuthorized(string apiKey, string[] roles) {

            throw new NotImplementedException();
        }
    }
}