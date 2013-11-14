using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheCowSample.Models
{
    public class AccommodationProperty
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }

        public Destination Destination { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}