using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheCowSample.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}