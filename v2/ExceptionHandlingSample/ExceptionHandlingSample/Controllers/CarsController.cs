using ExceptionHandlingSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace ExceptionHandlingSample.Controllers
{
    [SpecialToCarsControllerExceptionFilter]
    public class CarsController : ApiController
    {
        CarsContext _carsContext = new CarsContext();

        public CarsController()
        {
           // throw new DivideByZeroException();
        }

        public IEnumerable<Car> Get()
        {
            throw new InvalidOperationException();
            return _carsContext.All;
        }

        public Car Get(int id)
        {
            var car = _carsContext.GetSingle(x => x.Id == id);

            if (car == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Contact not found")
                };

                throw new HttpResponseException(response);
            }

            return car;
        }

        public HttpResponseMessage Post(Car car)
        {

            _carsContext.Add(car);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public Car Put(int id, Car car)
        {
            car.Id = id;
            if (!_carsContext.TryUpdate(car))
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Car not found")
                };

                throw new HttpResponseException(response);
            }

            return car;
        }

        public HttpResponseMessage Delete(int id)
        {
            if (!_carsContext.TryRemove(id))
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Contact not found")
                };

                throw new HttpResponseException(response);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

    public class SpecialToCarsControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is DivideByZeroException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.Conflict, "Your input conflicted with the current state of the system.");
            }
        }
    }
}