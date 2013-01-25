using ConstructControllerSeperatelySample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ConstructControllerSeperatelySample.Controllers {

    public class CarsController : ApiController {

        private readonly ILogger _logger;
        public CarsController(ILogger logger) {

            _logger = logger;
        }

        public string[] GetCars() {

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }

        public string[] GetCar(string id) {

            return new[] { 
                string.Format("Car {0}", id)
            };
        }
    }
}