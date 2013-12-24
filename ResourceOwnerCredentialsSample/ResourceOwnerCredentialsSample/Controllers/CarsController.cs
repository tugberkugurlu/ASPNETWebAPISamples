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
