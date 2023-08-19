using CSharpDemo.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpDemo
{
    public class IndexersDemo : DemoRunner<IndexersDemo>
    {
        [DemoCaption("Indexer of array of value type returns by reference")]
        public void Demo1()
        {
            var arr = new[] {
                new Point { X = 10, Y = 10 }
            };

            /*
             * The indexer of the array
             * returns the element by reference
             */

            // (10; 10)
            Console.WriteLine(arr[0]);

            arr[0].SetToZero();

            // (0; 0)
            Console.WriteLine(arr[0]);

            /*
             * Note:
             * If we do like
             * var p = arr[0];
             * p.SetToZero()
             * then
             * arr[0] stays untouched - (10, 10)
             */
        }

        [DemoCaption("Indexer of collection of value type returns by value")]
        public void Demo2()
        {
            var listPoint = new List<Point>() { new() { X = 10, Y = 10 } };

            /*
             * List and all other collections
             * returns the element by value
             */

            // (10; 10)
            Console.WriteLine(listPoint[0]);

            listPoint.First().SetToZero(); // the same as listPoint[0]

            // (10; 10)
            Console.WriteLine(listPoint[0]);
        }

        [DemoCaption("Create custom indexer")]
        public void Demo3()
        {
            var fib = new FibonacciList(140);

            Console.WriteLine(fib[1]);
            Console.WriteLine(fib[5]);
            Console.WriteLine(fib[10]);
            Console.WriteLine(fib[50]);
            Console.WriteLine(fib[100]);
            Console.WriteLine(fib[140]);

            /*
             * Output:
             * 0
             * 3
             * 34
             * 7778742049
             * 218922995834555169026
             * 50095301248058391139327916261
             */

            // new FibonacciList(141)
            // error
            // System.OverflowException: Value was either too large or too small for a Decimal.
        }

        struct Point
        {
            public int X;
            public int Y;

            public void SetToZero()
            {
                X = 0;
                Y = 0;
            }

            public override string ToString() => $"({X}; {Y})";
        }

        public class FibonacciList
        {
            private readonly List<decimal> _list;

            public FibonacciList(int count)
            {
                _list = new(count);

                _list = count switch
                {
                    0 => new(),
                    1 => new() { 0 },
                    2 => new() { 0, 1 },
                    _ => new() { 0, 1 }
                };

                FillList(count);
            }

            private void FillList(int count)
            {
                int i = 2;

                while (i < count)
                {
                    _list.Add(_list[i - 2] + _list[i - 1]);
                    i++;
                }
            }

            // Indexer declaration
            public decimal this[int index] => index - 1 < 0
                ? -1
                : _list[index - 1];
        }
    }
}
