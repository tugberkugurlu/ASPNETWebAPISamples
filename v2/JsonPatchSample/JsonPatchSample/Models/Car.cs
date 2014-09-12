
using System.Collections.Generic;

namespace JsonPatchSample.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
        public CarDetail Detail { get; set; }
    }

    public class CarDetail
    {
        public string Colour { get; set; }
        public string DoorsAmount { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}