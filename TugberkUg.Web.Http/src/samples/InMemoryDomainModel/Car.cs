using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InMemoryDomainModel {

    public class Car {

        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public string Colour { get; set; }
        public float Price { get; set; }
        public int Mileage { get; set; }
    }

}