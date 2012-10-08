using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Security;
using AuthorizeAttributeSample.Models;

namespace AuthorizeAttributeSample.APIs {

    public class CarsController : ApiController {

        private readonly CarsContext _carsContext = new CarsContext();

        public IEnumerable<Car> Get() {

            return _carsContext.All;
        }
    }
}