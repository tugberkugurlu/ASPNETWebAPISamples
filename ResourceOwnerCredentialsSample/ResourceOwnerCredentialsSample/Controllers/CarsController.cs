using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceOwnerCredentialsSample.Controllers
{
    public class CarsController : ApiController
    {
        [Authorize]
        public string[] GetCars()
        {
            return new[] 
            {
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}
