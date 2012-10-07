using ComplexTypeParamActionSelection.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPIDoodle;

namespace ComplexTypeParamActionSelection.Controllers {

    public class CarsByCategoryRequestCommand {

        public int CategoryId { get; set; }
        public int Page { get; set; }

        [Range(1, 50)]
        public int Take { get; set; }

        public string UniqueId {
            
            get {

                return string.Format("{0}_{1}_{2}",
                    CategoryId, Page, Take);
            }
        }

        [BindingInfo(NoBinding = true)]
        public string MySuperProperty { get; set; }
    }

    public class CarsByColorRequestCommand {

        public int ColorId { get; set; }
        public int Page { get; set; }

        [Range(1, 50)]
        public int Take { get; set; }

        public string UniqueId {

            get {

                return string.Format("{0}_{1}_{2}",
                    ColorId, Page, Take);
            }
        }

        [BindingInfo(NoBinding = true)]
        public string MySuperProperty { get; set; }
    }

    [InvalidModelStateFilter]
    public class CarsController : ApiController {

        // GET /api/cars?categoryId=23&page=2&take=12
        public string[] GetCarsByCategoryId(
            [FromUri]CarsByCategoryRequestCommand cmd) {

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }

        // GET /api/cars?colorId=23&page=2&take=12
        public string[] GetCarsByColorId(
            [FromUri]CarsByColorRequestCommand cmd) {

            return new[] { 
                "Car 1",
                "Car 2"
            };
        }
    }
}