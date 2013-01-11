using MultiTenantWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MultiTenantWebAPI.Controllers {
    
    public class CarsController :ApiController {

        private readonly ILoggerService _loggerService;

        public CarsController(ILoggerService loggerService) {

            _loggerService = loggerService;
        }

        public string[] Get() {

            _loggerService.Log("CarsController > Get");
            return new[] { "Car1", "Car2", "Car3" };
        }
    }
}