using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;

namespace TimeoutWithOwinLimits.Controllers
{
    public class CarsController : ApiController
    {
        public string[] GetCars(int waitFor)
        {
            Trace.TraceInformation("Web API in");
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