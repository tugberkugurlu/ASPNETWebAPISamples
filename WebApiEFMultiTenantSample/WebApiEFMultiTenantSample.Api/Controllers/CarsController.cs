using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiEFMultiTenantSample.Api.Services;

namespace WebApiEFMultiTenantSample.Api.Controllers {
    
    public class CarsController : ApiController {

        private readonly ILoggerService _loggerService;
        public CarsController(ILoggerService loggerService) {

            _loggerService = loggerService;
        }

        public string[] Get() {

            _loggerService.Log("CarsController.Get");

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}