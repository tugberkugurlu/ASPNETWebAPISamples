using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KatanaOWINSample.Models;
using KatanaOWINSample.Filters;

namespace KatanaOWINSample.APIs {

    [InvalidModelStateFilter]
    public class CarsController : ApiController {

        private readonly CarsContext _carsCtx = new CarsContext();
	
        // GET /api/cars
        public IEnumerable<Car> Get() {

            return _carsCtx.All;
        }

        // GET /api/cars/{id}
        public Car GetCar(int id) {

            var carTuple = _carsCtx.GetSingle(id);

            if (!carTuple.Item1) {

                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return carTuple.Item2;
        }

        // POST /api/cars
        public HttpResponseMessage PostCar(Car car) {

            var createdCar = _carsCtx.Add(car);
            var response = Request.CreateResponse(HttpStatusCode.Created, createdCar);
            response.Headers.Location = new Uri(
                Url.Link("DefaultHttpRoute", new { id = createdCar.Id }));

            return response;
        }

        // PUT /api/cars/{id}
        public Car PutCar(int id, Car car) {

            car.Id = id;

            if (!_carsCtx.TryUpdate(car)) {

                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return car;
        }

        // DELETE /api/cars/{id}
        public HttpResponseMessage DeleteCar(int id) {

            if (!_carsCtx.TryRemove(id)) {

                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}