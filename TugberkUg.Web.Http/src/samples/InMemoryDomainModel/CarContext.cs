using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InMemoryDomainModel {

    public class CarContext {

        //Dummy data is from:
        //http://www.mikesdotnetting.com/Article/97/Cascading-DropDownLists-with-jQuery-and-ASP.NET

        #region _seed
        List<Car> cars = new List<Car> {

            new Car { CarId = 1, Make="Audi",Model="A4",Year=1995,Doors=4,Colour="Red",Price=2995f,Mileage=122458},
            new Car { CarId = 2, Make="Ford",Model="Focus",Year=2002,Doors=5,Colour="Black",Price=3250f,Mileage=68500},
            new Car { CarId = 3, Make="BMW",Model="5 Series",Year=2006,Doors=4,Colour="Grey",Price=24950f,Mileage=19500},
            new Car { CarId = 4, Make="Renault",Model="Laguna",Year=2000,Doors=5,Colour="Red",Price=3995f,Mileage=82600},
            new Car { CarId = 5, Make="Toyota",Model="Previa",Year=1998,Doors=5,Colour="Green",Price=2695f,Mileage=72400},
            new Car { CarId = 6, Make="Mini",Model="Cooper",Year=2005,Doors=2,Colour="Grey",Price=9850f,Mileage=19800},
            new Car { CarId = 7, Make="Mazda",Model="MX 5",Year=2003,Doors=2,Colour="Silver",Price=6995f,Mileage=51988},
            new Car { CarId = 8, Make="Ford",Model="Fiesta",Year=2004,Doors=3,Colour="Red",Price=3759f,Mileage=50000},
            new Car { CarId = 9, Make="Honda",Model="Accord",Year=1997,Doors=4,Colour="Silver",Price=1995f,Mileage=99750},
            new Car { CarId = 10, Make="Audi",Model="A6",Year=2005,Doors=5,Colour="Silver",Price=22995f,Mileage=25400},
            new Car { CarId = 11, Make="Jaguar",Model="XJS",Year=1992,Doors=4,Colour="Green",Price=3450,Mileage=92000},
            new Car { CarId = 12, Make="Jaguar",Model="X Type",Year=2006,Doors=4,Colour="Grey",Price=9950f,Mileage=17000},
            new Car { CarId = 13, Make="Renault",Model="Megane",Year=2007,Doors=5,Colour="Red",Price=8995f,Mileage=8500},
            new Car { CarId = 14, Make="Peugeot",Model="406",Year=2003,Doors=4,Colour="Grey",Price=3450f,Mileage=86000},
            new Car { CarId = 15, Make="Mini",Model="Cooper S",Year=2008,Doors=2,Colour="Black",Price=14850f,Mileage=9500},
            new Car { CarId = 16, Make="Mazda",Model="5",Year=2006,Doors=5,Colour="Silver",Price=6940f,Mileage=53500},
            new Car { CarId = 17, Make="Vauxhall",Model="Vectra",Year=2007,Doors=5,Colour="White",Price=13750f,Mileage=31000},
            new Car { CarId = 18, Make="Ford",Model="Puma",Year=1998,Doors=3,Colour="Silver",Price=2995f,Mileage=84500},
            new Car { CarId = 19, Make="Ford",Model="Ka",Year=2004,Doors=3,Colour="Red",Price=2995f,Mileage=61000},
            new Car { CarId = 20, Make="Ford",Model="Focus",Year=2007,Doors=5,Colour="Blue",Price=9950f,Mileage=19000},
            new Car { CarId = 21, Make="BMW",Model="3 Series",Year=2001,Doors=4,Colour="White",Price=5950f,Mileage=98000},
            new Car { CarId = 22, Make="Citroen",Model="C5",Year=2005,Doors=5,Colour="Silver",Price=5995f,Mileage=38400},
            new Car { CarId = 23, Make="Toyota",Model="Corolla T3",Year=2004,Doors=5,Colour="Blue",Price=5995f,Mileage=71000},
            new Car { CarId = 24, Make="Toyota",Model="Yaris",Year=2005,Doors=3,Colour="Grey",Price=5350f,Mileage=39000},
            new Car { CarId = 25, Make="Porsche",Model="911",Year=2003,Doors=2,Colour="Red",Price=16995f,Mileage=88000},
            new Car { CarId = 26, Make="Ford",Model="Fiesta",Year=2004,Doors=3,Colour="Red",Price=5759f,Mileage=49000},
            new Car { CarId = 27, Make="Honda",Model="Accord",Year=1996,Doors=4,Colour="Black",Price=1995f,Mileage=105000},
            new Car { CarId = 28, Make="Audi",Model="A3 Avant",Year=2005,Doors=5,Colour="Blue",Price=12995f,Mileage=22458},
            new Car { CarId = 29, Make="Ford",Model="Mondeo",Year=2007,Doors=5,Colour="Gold",Price=12250f,Mileage=8500},
            new Car { CarId = 30, Make="BMW",Model="1 Series",Year=2006,Doors=4,Colour="Black",Price=16950f,Mileage=19500},
            new Car { CarId = 31, Make="Renault",Model="Clio",Year=2005,Doors=3,Colour="Red",Price=5995f,Mileage=32600},
            new Car { CarId = 32, Make="Toyota",Model="Verso",Year=2008,Doors=5,Colour="White",Price=12995f,Mileage=5800},
            new Car { CarId = 33, Make="Mini",Model="Cooper",Year=2003,Doors=2,Colour="Black",Price=7950f,Mileage=36800},
            new Car { CarId = 34, Make="Mazda",Model="6",Year=2007,Doors=4,Colour="Blue",Price=16995f,Mileage=11300},
            new Car { CarId = 35, Make="Ford",Model="Mondeo",Year=2004,Doors=5,Colour="Green",Price=8759f,Mileage=66000},
            new Car { CarId = 36, Make="Honda",Model="Civic",Year=1997,Doors=4,Colour="Grey",Price=1995f,Mileage=99750},
            new Car { CarId = 37, Make="Audi",Model="Q7",Year=2005,Doors=5,Colour="Black",Price=22995f,Mileage=25400},
            new Car { CarId = 38, Make="Jaguar",Model="XK8",Year=1992,Doors=4,Colour="Blue",Price=3450,Mileage=92000},
            new Car { CarId = 39, Make="Jaguar",Model="S Type",Year=2006,Doors=4,Colour="Red",Price=9950f,Mileage=17000},
            new Car { CarId = 40, Make="Renault",Model="Megane",Year=2007,Doors=5,Colour="Yellow",Price=8995f,Mileage=8500},
            new Car { CarId = 41, Make="Peugeot",Model="406",Year=2003,Doors=4,Colour="White",Price=3450f,Mileage=86000},
            new Car { CarId = 42, Make="Mini",Model="Cooper",Year=2008,Doors=2,Colour="Red",Price=14850f,Mileage=9500},
            new Car { CarId = 43, Make="Mazda",Model="5",Year=2006,Doors=5,Colour="White",Price=6940f,Mileage=53500},
            new Car { CarId = 44, Make="Vauxhall",Model="Vectra",Year=2007,Doors=5,Colour="Blue",Price=13750f,Mileage=31000},
            new Car { CarId = 45, Make="Ford",Model="Puma",Year=1998,Doors=3,Colour="Red",Price=2995f,Mileage=84500},
            new Car { CarId = 46, Make="Ford",Model="Ka",Year=2004,Doors=3,Colour="Black",Price=2995f,Mileage=61000},
            new Car { CarId = 47, Make="Ford",Model="Focus",Year=2007,Doors=5,Colour="Grey",Price=9950f,Mileage=19000},
            new Car { CarId = 48, Make="BMW",Model="3 Series",Year=2001,Doors=4,Colour="Red",Price=5950f,Mileage=98000},
            new Car { CarId = 49, Make="Citroen",Model="C5",Year=2005,Doors=5,Colour="Yellow",Price=5995f,Mileage=38400},
            new Car { CarId = 50, Make="Toyota",Model="Corolla T3",Year=2004,Doors=5,Colour="Red",Price=5995f,Mileage=71000},
            new Car { CarId = 51, Make="Toyota",Model="Yaris",Year=2005,Doors=3,Colour="Black",Price=5350f,Mileage=39000},
            new Car { CarId = 52, Make="Porsche",Model="911",Year=2003,Doors=2,Colour="White",Price=16995f,Mileage=88000},
            new Car { CarId = 53, Make="Ford",Model="Fiesta",Year=2004,Doors=3,Colour="Grey",Price=5759f,Mileage=49000},
            new Car { CarId = 54, Make="Honda",Model="Accord",Year=1996,Doors=4,Colour="Green",Price=1995f,Mileage=105000}

        }; 
        #endregion

        public IEnumerable<Car> GetAll() {

            return
                cars;
        }

        public Car GetSingle(int id) {

            return
                cars.FirstOrDefault(x => x.CarId == id);
        }
    }
}
