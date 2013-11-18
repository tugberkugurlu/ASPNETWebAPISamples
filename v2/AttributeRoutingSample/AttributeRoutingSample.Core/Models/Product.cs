using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttributeRoutingSample.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }

        public Store Store { get; set; }
    }
}