using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheCowSample.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Destination> Destinations { get; set; }
    }
}