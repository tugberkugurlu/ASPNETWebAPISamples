using AttributeRoutingSample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace AttributeRoutingSample.Controllers
{
    public class StoreDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StoresController : ApiController
    {
        public readonly MyStoreContext _ctx = new MyStoreContext();

        public IEnumerable<StoreDto> GetStores()
        {
            IEnumerable<Store> stores = _ctx.Stores.ToArray();
            return stores.Select(x => new StoreDto { Id = x.Id, Name = x.Name });
        }

        [ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetStore(int id)
        {
            Store store = _ctx.Stores.FirstOrDefault(x => x.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.OK, new ProductDto { Id = store.Id, Name = store.Name });
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