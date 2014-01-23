using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HttpPatchASPNETWebApi.Models;
using HttpPatchASPNETWebApi.Filters;
using System.Reflection;
using System.Collections.Concurrent;

namespace HttpPatchASPNETWebApi.APIs
{

    [InvalidModelStateFilter]
    public class CarsController : ApiController
    {
        private readonly CarsContext _carsCtx = new CarsContext();

        // GET /api/cars
        public IEnumerable<Car> Get()
        {
            return _carsCtx.All;
        }

        // GET /api/cars/{id}
        public Car GetCar(int id)
        {
            var carTuple = _carsCtx.GetSingle(id);

            if (!carTuple.Item1)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return carTuple.Item2;
        }

        // POST /api/cars
        public HttpResponseMessage PostCar(Car car)
        {
            var createdCar = _carsCtx.Add(car);
            var response = Request.CreateResponse(HttpStatusCode.Created, createdCar);
            response.Headers.Location = new Uri(
                Url.Link("DefaultHttpRoute", new { id = createdCar.Id }));

            return response;
        }

        // PUT /api/cars/{id}
        public Car PutCar(int id, Car car)
        {
            car.Id = id;

            if (!_carsCtx.TryUpdate(car))
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return car;
        }

        // PATCH /api/cars/{id}
        public Car PatchCar(int id, CarPatch car)
        {
            var carTuple = _carsCtx.GetSingle(id);
            if (!carTuple.Item1)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            Patch<CarPatch, Car>(car, carTuple.Item2);

            // Not required but better to put here to simulate the external storage.
            if (!_carsCtx.TryUpdate(carTuple.Item2))
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return carTuple.Item2;
        }

        // DELETE /api/cars/{id}
        public HttpResponseMessage DeleteCar(int id)
        {
            if (!_carsCtx.TryRemove(id))
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // private helpers

        private static ConcurrentDictionary<Type, PropertyInfo[]> TypePropertiesCache = 
            new ConcurrentDictionary<Type, PropertyInfo[]>();

        private void Patch<TPatch, TEntity>(TPatch patch, TEntity entity)
            where TPatch : class
            where TEntity : class
        {
            PropertyInfo[] properties = TypePropertiesCache.GetOrAdd(
                patch.GetType(), 
                (type) => type.GetProperties(BindingFlags.Instance | BindingFlags.Public));

            foreach (PropertyInfo prop in properties)
            {
                PropertyInfo orjProp = entity.GetType().GetProperty(prop.Name);
                object value = prop.GetValue(patch);
                if (value != null)
                {
                    orjProp.SetValue(entity, value);
                }
            }
        }
    }
}