using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace ParallelOwinTesting.Api.Data
{
    public class SafeCarsContext
    {
        private static int _nextId = 9;
        private static object _incLock = new object();

        // cars store
        private readonly static ConcurrentDictionary<int, Car> _carsDictionary = new ConcurrentDictionary<int, Car>(new HashSet<KeyValuePair<int, Car>> { 
            new KeyValuePair<int, Car>(1, new Car { Id = 1, Make = "Make1", Model = "Model1", Year = 2010, Price = 10732.2F }),
            new KeyValuePair<int, Car>(2, new Car { Id = 2, Make = "Make2", Model = "Model2", Year = 2008, Price = 27233.1F }),
            new KeyValuePair<int, Car>(3, new Car { Id = 3, Make = "Make3", Model = "Model1", Year = 2009, Price = 67437.0F }),
            new KeyValuePair<int, Car>(4, new Car { Id = 4, Make = "Make4", Model = "Model3", Year = 2007, Price = 78984.2F }),
            new KeyValuePair<int, Car>(5, new Car { Id = 5, Make = "Make5", Model = "Model1", Year = 1987, Price = 56200.89F }),
            new KeyValuePair<int, Car>(6, new Car { Id = 6, Make = "Make6", Model = "Model4", Year = 1997, Price = 46003.2F }),
            new KeyValuePair<int, Car>(7, new Car { Id = 7, Make = "Make7", Model = "Model5", Year = 2001, Price = 78355.92F }),
            new KeyValuePair<int, Car>(8, new Car { Id = 8, Make = "Make8", Model = "Model1", Year = 2011, Price = 423223.23F })
        });

        public IEnumerable<Car> All
        {
            get
            {
                return _carsDictionary.Values;
            }
        }

        public IEnumerable<Car> Get(Func<Car, bool> predicate)
        {
            return _carsDictionary.Values.Where(predicate);
        }

        public Tuple<bool, Car> GetSingle(int id)
        {
            Car car;
            bool doesExist = _carsDictionary.TryGetValue(id, out car);
            return new Tuple<bool, Car>(doesExist, car);
        }

        public Car GetSingle(Func<Car, bool> predicate)
        {
            return _carsDictionary.Values.FirstOrDefault(predicate);
        }

        public Car Add(Car car)
        {
            lock (_incLock)
            {
                car.Id = _nextId;
                _carsDictionary.TryAdd(car.Id, car);
                _nextId++;
            }

            return car;
        }

        public bool TryRemove(int id)
        {
            Car removedCar;
            return _carsDictionary.TryRemove(id, out removedCar);
        }

        public bool TryUpdate(Car car)
        {
            Car oldCar;
            if (_carsDictionary.TryGetValue(car.Id, out oldCar))
            {
                return _carsDictionary.TryUpdate(car.Id, car, oldCar);
            }

            return false;
        }
    }
}