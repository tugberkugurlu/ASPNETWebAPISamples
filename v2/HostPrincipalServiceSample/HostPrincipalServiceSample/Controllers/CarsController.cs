using System.Web.Http;

namespace HostPrincipalServiceSample.Controllers
{
    [Authorize]
    public class CarsController : ApiController
    {
        public string[] GetCars()
        {
            return new[]
            {
                "Cars 1",
                "Cars 2"
            };
        }
    }
}