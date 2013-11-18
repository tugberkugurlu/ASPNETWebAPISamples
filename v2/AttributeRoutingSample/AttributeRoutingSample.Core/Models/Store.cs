using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttributeRoutingSample.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}