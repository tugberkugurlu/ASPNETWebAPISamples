using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.SelfHost;
using System.Web.Http.Tracing;

namespace WebApıTracingInfo {

    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductsController : ApiController {

        Product[] products = new Product[]  
        {  
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },  
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },  
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }  
        };

        public IEnumerable<Product> GetAllProducts() {
            return products;
        }

        public Product GetProductById(int id) {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category) {
            return products.Where(p => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }
    }

    class Program {

        //http://blogs.msdn.com/b/roncain/archive/2012/04/12/tracing-in-asp-net-web-api.aspx
        static void Main(string[] args) {

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:8989");
            config.Services.Replace(typeof(ITraceWriter), new SimpleConsoleTraceWriter());
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute", 
                "api/{controller}/{id}", 
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config)) {

                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }

    public class SimpleConsoleTraceWriter : ITraceWriter {

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction) {
            
            TraceRecord traceRecord = new TraceRecord(request, category, level);
            traceAction(traceRecord);
            ShowTrace(traceRecord);
        }

        private void ShowTrace(TraceRecord traceRecord) {

            Console.WriteLine(
                string.Format(
                    "Id:{9}\n{0} {1}\nCategory: {2}\nLevel: {3}\n{4} {5} {6} {7}\n{8}\n{8}",
                    traceRecord.Request.Method.ToString(),
                    traceRecord.Request.RequestUri.ToString(),
                    traceRecord.Category,
                    traceRecord.Level,
                    traceRecord.Kind,
                    traceRecord.Operator,
                    traceRecord.Operation,
                    traceRecord.Exception != null 
                        ? traceRecord.Exception.GetBaseException().Message
                        : !string.IsNullOrEmpty(traceRecord.Message)
                            ? traceRecord.Message
                            : string.Empty,
                    "-------------------------------------------------------",
                    traceRecord.Request.Properties[HttpPropertyKeys.RequestCorrelationKey]
                ));
        }
    }
}