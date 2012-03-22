using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using InMemoryDomainModel;
using TugberkUg.Web.Http.Filters;

namespace CSVMediaTypeFormatterSample.APIs {

    public class CarsController : ApiController {
        
        private CarContext ctx = new CarContext();

        public IList<Car> GetCars() {

            return
                ctx.GetAll().ToList();
        }

        public Car GetCar(int id) {

            return
                ctx.GetSingle(id);
        }
    }
}