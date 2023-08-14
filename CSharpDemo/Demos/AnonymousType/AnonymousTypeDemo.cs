using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.AnonymousType
{
    public class AnonymousTypeDemo : DemoRunner<AnonymousTypeDemo>
    {
        [DemoCaption("Creating anonymous type")]
        public void Demo1()
        {
            var car = new { Model = "Nissan", Year = 2020, Color = "Dark brown" };

            Console.WriteLine(car); // { Model = Nissan, Year = 2020, Color = Dark brown }
        }

        class Car
        { 
            public string Model { get; set; }
            public int Year { get; set; }
            public string Color { get; set; }

            public List<string> Owners { get; set; }
        }

        [DemoCaption("Creating anon type: inferred property names")]
        public void Demo2()
        {
            var car = new Car() { Model = "Nissan", Year = 2020, Color = "Dark brown", Owners = new List<string>() { "John", "James", "Jim" } };
            var nissan = new { 
                car.Model,
                car.Year,
                car.Color,
                Owners = string.Join(", ", car.Owners)
            };

            Console.WriteLine(nissan);
        }


        [DemoCaption("Comparing two anonymous types")]
        public void Demo3()
        {
            var car1 = new { Model = "Nissan", Year = 2020, Color = "Dark brown" };
            var car2 = new { Model = "Nissan", Year = 2020, Color = "Dark brown" };

            Console.WriteLine(car1 == car2); // false (comparing by reference)
            Console.WriteLine(car1.Equals(car2)); // true (comparing by values)
        }

        [DemoCaption("Anonymous type using with LINQ")]
        public void Demo4()
        {
            List<Car> cars = new List<Car>()
            {
                new Car() { Model = "Nissan", Year = 2019, Color = "Green" },
                new Car() { Model = "Renault", Year = 2012, Color = "Black" },
                new Car() { Model = "Toyota", Year = 2015, Color = "Red" },
            };

            ConsoleHelper.WriteLineCollection(cars
                .Select(car => new { car.Year, car.Model })
                .OrderBy(x => x.Year)
                .ToList());
            
        }

        [DemoCaption("Anonymous type: non-destructive mutation - with expression")]
        public void Demo5()
        {
            var oldCar = new { Model = "Nissan", Year = 2010, Color = "Green" };

            // We can't mutate the original instance, cause it's read-only
            // Instead, it's possible to use With keyword to create a copy of an anonymous type
            // with some different values

            var newCar = oldCar with { Year = 2023 };

            Console.WriteLine("{0} - Old car", oldCar);
            Console.WriteLine("{0} - Car with changed Year property", newCar);

            // Immutability is beneficial in multi-threaded applications

        }

        [DemoCaption("Anonymous type: with expression - reference copying")]
        public void Demo6()
        {
            string[] ownersList = new[] { "John", "Jake", "Jane" };

            var car1 = new { Model = "Nissan", Year = 2010, Owners = ownersList };
            var car2 = car1 with { };

            ConsoleHelper.WriteCollection(car1.Owners); // John, Jake, Jane
            ConsoleHelper.WriteCollection(car2.Owners); // John, Jake, Jane

            // Now we have instance of an anonymous type - car1 and a copy - car2.
            // But since we have reference type property called Owners, we copy just reference

            // Let's update ownerList array with new values

            ownersList[0] = "Bob";
            ownersList[1] = "Bill";
            ownersList[2] = "Brian";

            ConsoleHelper.WriteCollection(car1.Owners);   // Bob, Bill, Brian
            ConsoleHelper.WriteCollection(car2.Owners);   // Bob, Bill, Brian

        }
    }
}
