using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizeAttributeSample.Models {

    public class Car {

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }
    }

    public class CarsContext {

        //data store
        readonly static List<Car> cars = new List<Car> { 
            new Car { Id = 1, Make = "Make1", Model = "Model1", Year = 2010, Price = 10732.2F },
            new Car { Id = 2, Make = "Make2", Model = "Model2", Year = 2008, Price = 27233.1F },
            new Car { Id = 3, Make = "Make3", Model = "Model1", Year = 2009, Price = 67437.0F },
            new Car { Id = 4, Make = "Make4", Model = "Model3", Year = 2007, Price = 78984.2F },
            new Car { Id = 5, Make = "Make5", Model = "Model1", Year = 1987, Price = 56200.89F },
            new Car { Id = 6, Make = "Make6", Model = "Model4", Year = 1997, Price = 46003.2F },
            new Car { Id = 7, Make = "Make7", Model = "Model5", Year = 2001, Price = 78355.92F },
            new Car { Id = 8, Make = "Make8", Model = "Model1", Year = 2011, Price = 1823223.23F }
        };

        public IEnumerable<Car> All {
            get {
                return cars;
            }
        }

        public IEnumerable<Car> Get(Func<Car, bool> predicate) {

            return cars.Where(predicate);
        }

        public Car GetSingle(Func<Car, bool> predicate) {

            return cars.FirstOrDefault(predicate);
        }

        public void Add(Car car) {

            int maxId = 1;

            if (cars.Any()) {

                maxId = cars.Max(x => x.Id);
            }

            car.Id = maxId + 1;

            cars.Add(car);
        }
    }
}