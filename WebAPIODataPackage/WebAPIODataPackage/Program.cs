using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.SelfHost;

namespace WebAPIODataPackage {

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

        [Queryable]
        public IQueryable<Product> GetAllProducts() {
            return products.AsQueryable();
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

        //http://blogs.msdn.com/b/alexj/archive/2012/08/15/odata-support-in-asp-net-web-api.aspx
        static void Main(string[] args) {

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration("http://localhost:8989");
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Product>("Products");
            IEdmModel model = modelBuilder.GetEdmModel();

            using (HttpSelfHostServer server = new HttpSelfHostServer(config)) {

                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}