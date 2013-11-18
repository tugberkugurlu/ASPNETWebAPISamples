using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AttributeRoutingSample.Models
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext() : base(@"Server=.\SQL12; Initial Catalog=MyDbContext; Integrated Security=True")
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}