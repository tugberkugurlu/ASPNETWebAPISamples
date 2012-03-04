using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApiKeyAuthAttributeSample.Infrastructure;
using TugberkUg.Web.Http.Filters;

namespace ApiKeyAuthAttributeSample.APIs {

    [ApiKeyAuth("apiKey", typeof(InMemoryApiKeyAuthorizer), Roles = "Admin")]
    public class CarsController : ApiController {

        public string[] GetCars() {

            return new string[] { 
                "BMW",
                "FIAT",
                "Mercedes"
            };
        }

        public string GetCar(int id) {

            return
                "BMW";
        }
    }
}