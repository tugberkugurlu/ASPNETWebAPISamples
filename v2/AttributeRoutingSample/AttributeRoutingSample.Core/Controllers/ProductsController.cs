using AttributeRoutingSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace AttributeRoutingSample.Controllers
{
    public interface IRequestModel
    {
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
    }

    public abstract class ProductBaseRequestModel : IRequestModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class ProductRequestModel : ProductBaseRequestModel
    {
        /// <summary>
        /// Foo Bar
        /// </summary>
        [Required]
        public int? StoreId { get; set; }
    }

    public class ProductUpdateRequestModel : ProductBaseRequestModel
    {
    }

    /// <summary>
    /// Products API endpoints allows you to query, create and delete products.
    /// </summary>
    public class ProductsController : ApiController
    {
        public readonly MyStoreContext _ctx = new MyStoreContext();

        public IEnumerable<ProductDto> GetProducts()
        {
            IEnumerable<Product> stores = _ctx.Products.ToArray();
            return stores.Select(x => new ProductDto { Id = x.Id, StoreId = x.StoreId, Name = x.Name });
        }

        [ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.OK, new ProductDto { Id = product.Id, StoreId = product.StoreId, Name = product.Name });
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

            IEnumerable<Product> products = _ctx.Products.Where(x => x.StoreId == storeId).ToArray();
            return Content(HttpStatusCode.OK, products.Select(x => new ProductDto { Id = x.Id, StoreId = x.StoreId, Name = x.Name }).ToArray());
        }

        [ResponseType(typeof(ProductDto))]
        public IHttpActionResult PostProduct(ProductRequestModel requestModel)
        {
            Product product = new Product { StoreId = requestModel.StoreId.Value, Name = requestModel.Name };
            _ctx.Products.Add(product);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return Created(
                Url.Link("DefaultApi", new { controller = "products", id = product.Id }),
                new ProductDto { Id = product.Id, StoreId = product.StoreId, Name = product.Name });
        }

        [ResponseType(typeof(ProductDto))]
        public IHttpActionResult PutProduct(int id, ProductUpdateRequestModel requestModel)
        {
            Product product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = requestModel.Name;

            try
            {
                _ctx.Entry(product).State = EntityState.Modified;
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return Content(HttpStatusCode.OK, new ProductDto { Id = product.Id, StoreId = product.StoreId, Name = product.Name });
        }

        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _ctx.Products.Remove(product);

            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
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