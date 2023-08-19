using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Indexers__Ranges__Indices
{
    public partial class IndexersDemo
    {
        [DemoCaption("Ranges")]
        public void Demo4()
        {
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7 };

            var arr1 = arr[1..];

            // 2 3 4 5 6 7
            ConsoleHelper.WriteCollection(arr1);

            var arr2 = arr[1..3];

            // 2 3
            ConsoleHelper.WriteCollection(arr2);

            var arr3 = arr[..^3];

            // 1 2 3 4
            ConsoleHelper.WriteCollection(arr3);

            var arr4 = arr[^3..];

            // 5 6 7
            ConsoleHelper.WriteCollection(arr4);

            var arr5 = arr[^3];

            // 5
            Console.WriteLine(arr5);
        }
    }
}
