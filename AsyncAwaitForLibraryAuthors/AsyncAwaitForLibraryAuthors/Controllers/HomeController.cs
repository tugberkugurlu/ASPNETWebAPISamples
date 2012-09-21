using SampleAPI.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncAwaitForLibraryAuthors.Controllers {

    public class HomeController : Controller {

        public async Task<ViewResult> CarsAsync() {

            SampleAPIClient client = new SampleAPIClient();
            var cars = await client.GetCarsAsync();

            return View("Index", model: cars);
        }

        public ViewResult CarsSync() {

            SampleAPIClient client = new SampleAPIClient();
            var cars = client.GetCarsAsync().Result;

            return View("Index", model: cars);
        }
    }
}