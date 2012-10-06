using ComplexTypeParamActionSelection.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPIDoodle.Controllers;

namespace ComplexTypeParamActionSelection.Controllers {

    public class CarsByCategoryRequestCommand {

        public int CategoryId { get; set; }
        public int Page { get; set; }

        [Range(1, 50)]
        public int Take { get; set; }
    }

    public class CarsByColorRequestCommand {

        public int ColorId { get; set; }
        public int Page { get; set; }

        [Range(1, 50)]
        public int Take { get; set; }
    }

    [InvalidModelStateFilter]
    public class CarsController : ApiController {

        public string[] GetCarsByCategoryId(
            [FromUri]CarsByCategoryRequestCommand cmd) {

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }

        public string[] GetCarsByColorId(
            [FromUri]CarsByColorRequestCommand cmd) {

            return new[] { 
                "Car 1",
                "Car 2"
            };
        }
    }
}