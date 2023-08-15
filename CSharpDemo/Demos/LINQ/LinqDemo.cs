using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.LINQ
{
    public class LinqDemo : DemoRunner<LinqDemo>
    {
        public void Demo1()
        {
            var list = new List<int>() { 0, 0, 0, 1 };

            var filtered = list.Where(w => w == 0);

            for (var i = 0; i < filtered.Count(); i++)
            {
                list.Remove(0);
            }

            Console.WriteLine(list.Count());
        }
    }
}
