using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ConnegAlgorithmSample.APIs {

    public class CarsController : ApiController {

        public string[] Get() {

            return new string[] { 
                "BMW",
                "Ferrari",
                "FIAT"
            };
        }
    }
}