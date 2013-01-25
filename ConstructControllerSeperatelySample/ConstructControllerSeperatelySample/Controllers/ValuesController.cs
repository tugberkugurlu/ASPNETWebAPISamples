using ConstructControllerSeperatelySample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ConstructControllerSeperatelySample.Controllers {

    public class ValuesController : ApiController {

        private readonly ILogger _logger;
        public ValuesController(ILogger logger) {

            _logger = logger;
        }

        public string[] GetValues() {

            return new[] { 
                "Value 1",
                "Value 2",
                "Value 3"
            };
        }
    }
}