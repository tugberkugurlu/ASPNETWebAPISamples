using System;
using System.Threading;
using System.Web.Http;

namespace AspNetTimeoutSample.Controllers
{
    public class CarsController : ApiController
    {
        public string[] GetCars(int waitFor)
        {
            Thread.Sleep(TimeSpan.FromSeconds(waitFor));

            return new[]
            {
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}