using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CacheCowSample.Models
{
    public class ReservationContext : DbContext
    {
        public ReservationContext() : base(@"Server=.\SQL12;Initial Catalog=CacheCowSample;Integrated Security=True")
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<AccommodationProperty> AccommodationProperties { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}