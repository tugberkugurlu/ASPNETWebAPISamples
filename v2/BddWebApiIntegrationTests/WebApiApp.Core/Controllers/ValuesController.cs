using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiApp.Core.Controllers
{
    public class ValuesController : ApiController
    {
        public IHttpActionResult Get() 
        {
            var content = new[] 
            { 
                "Car 1",
                "Car 2",
                "Car 3"
            };

            return Content(HttpStatusCode.OK, content);
        }
    }
}
