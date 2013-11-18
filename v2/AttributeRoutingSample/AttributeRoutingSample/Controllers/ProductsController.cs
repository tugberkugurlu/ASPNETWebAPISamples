using AttributeRoutingSample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace AttributeRoutingSample.Controllers
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductsController : ApiController
    {
        public readonly MyStoreContext _ctx = new MyStoreContext();

        public IEnumerable<ProductDto> GetProducts()
        {
            IEnumerable<Product> stores = _ctx.Products.ToArray();
            return stores.Select(x => new ProductDto { Id = x.Id, Name = x.Name });
        }

        [ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.OK, new ProductDto { Id = product.Id, Name = product.Name });
        }

        [Route("stores/{storeId:int:min(1)}/products")]
        [ResponseType(typeof(IEnumerable<ProductDto>))]
        public IHttpActionResult GetProductsByStoreId(int storeId)
        {
            Store store = _ctx.Stores.FirstOrDefault(x => x.Id == storeId);
            if (store == null)
            {
                return NotFound();
            }

            IEnumerable<Product> stores = _ctx.Products.Where(x => x.StoreId == storeId).ToArray();
            return Content(HttpStatusCode.OK, stores.Select(x => new ProductDto { Id = x.Id, Name = x.Name }).ToArray());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ctx.Dispose();
            }
        }
    }
}