using System.Runtime.InteropServices.ComTypes;
using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Indexers__Ranges__Indices
{
    public partial class IndexersDemo
    {
        [DemoCaption("Range")]
        public void Demo4()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };

            /*
             * Range - readonly struct.
             * Range operator .., which specifies the start and end of a range
             * Start - inclusive
             * End - exclusive
             */

            var range1To3 = new Range(1, 3);
            var range0To3 = ..3;

            // 2 3
            ConsoleHelper.WriteCollection(arr[range1To3]);  // 2 3
            ConsoleHelper.WriteCollection(arr[range0To3]);  // 1 2 3

            Console.WriteLine(range1To3.Equals(new Range(1, 3))); // true;

            /*
               Using range syntax may causes extra allocations and copies, impacting performance.
               In performance sensitive code,
               consider using Span<T> or Memory<T> as the sequence type,
               since the range operator does not allocate for them
             */
        }

        [DemoCaption("Indices")]
        public void Demo5()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };

            /*
             * The index from end operator ^,
             * which specifies that an index is relative to the end of a sequence
             */

            Index index1 = ^1;
            Index index2 = new Index(1, true);

            Console.WriteLine(arr[index1]); // 5
            Console.WriteLine(arr[index2]); // 5

        }

        [DemoCaption("Indices and ranges")]
        public void Demo6()
        {
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7 };

            var arrays = new List<int[]>()
            {
                arr[1..],
                arr[1..3],
                arr[..^3],
                arr[^3..],
                arr[^3..^1],
                arr[..^3],
                arr[2..^3],
            };

            ConsoleHelper.WriteLineCollection(arrays);

            /*
               Output:
               2 3 4 5 6 7
               2 3
               1 2 3 4
               5 6 7
               5 6
               1 2 3 4
               3 4
             */

        }
    }
}
