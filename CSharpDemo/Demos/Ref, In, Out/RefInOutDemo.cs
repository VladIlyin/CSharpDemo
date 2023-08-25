using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Ref__In__Out
{
    public class RefInOutDemo : DemoRunner<RefInOutDemo>
    {
        public record Person()
        {
            public string Name { get; set; }
        }

        [DemoCaption("Pass reference type to a method with and without ref")]
        public void Demo1()
        {
            var person = new Person() { Name = "Alice" };

            // changed
            ChangePerson(person, "Bob");
            Console.WriteLine(person); // Person { name = Bob }

            // not changed
            ChangePersonWithNew(person, "Carol");
            Console.WriteLine(person); // Person { name = Bob }

            // changed
            ChangePersonByRef(ref person, "Dave");
            Console.WriteLine(person); // Person { name = Dave }

            // change person property
            void ChangePerson(Person person, string name)
            {
                person.Name = name;
            }

            // assign new person instance to parameter
            void ChangePersonWithNew(Person person, string name)
            {
                person = new Person() { Name = name };
            }

            // assign new person instance to parameter passed by ref
            void ChangePersonByRef(ref Person person, string name)
            {
                person = new Person() { Name = name };
            }
        }
        
        public record struct Point(int x, int y);

        [DemoCaption("Pass value type to a method with and without ref")]
        public void Demo2()
        {
            var point = new Point(1, 1);

            ChangePoint(point, 10, 10);
            Console.WriteLine(point); // Point { x = 1, y = 1 }

            ChangePointByRef(ref point, 10, 10);
            Console.WriteLine(point); // Point { x = 10, y = 10 }

            void ChangePoint(Point point, int x, int y)
            {
                point = new Point(x, y);

                // or
                //point.x = x;
                //point.y = y;
            }

            void ChangePointByRef(ref Point point, int x, int y)
            {
                point = new Point(x, y);
                
                // or
                //point.x = x;
                //point.y = y;
            }
        }
    }
}
