using CSharpDemo.Helpers;

namespace CSharpDemo
{
    public class IndexersDemo : DemoRunner<IndexersDemo>
    {
        [DemoCaption("Demo 1")]
        public void Demo1()
        {

        }

        // The indexer of the array returns the element by reference
        // List and all other collections returns the element by value
        [DemoCaption("Indexers Demo: indexer returns reference or value")]
        public void Demo10()
        {
            var arr = new Point[] {
                new Point { X = 10, Y = 10 }
            };

            arr[0].Reset();             // return array item by ref
            Console.WriteLine(arr[0]);  // (0; 0)

            

            var listPoint = new List<Point>() { new Point { X = 10, Y = 10 } };

            // listPoint[0].X = 0; // // error CS1612: Cannot modify the return value because it is not a variable

            var point = listPoint[0]; // return list item by value
            point.Reset();

            Console.WriteLine(point);           // (0; 0)
            Console.WriteLine(listPoint[0]);    // (10; 10)
        }

        struct Point
        {
            public int X;
            public int Y;

            public void Reset()
            {
                X = 0;
                Y = 0;
            }

            public override string ToString() => $"({X}; {Y})";
        }
    }
}
