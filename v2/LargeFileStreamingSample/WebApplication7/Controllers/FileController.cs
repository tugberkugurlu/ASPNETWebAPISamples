using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication7.Controllers
{
    public class FileController : ApiController
    {
        public HttpResponseMessage Get()
        {
            Stream streamToReadFrom = File.OpenRead(@"C:\Users\Tugberk\AppData\Local\Temp\tmpEE38.tmp");
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(streamToReadFrom);

            return response;
        }
    }
}
