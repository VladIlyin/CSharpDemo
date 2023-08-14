using CSharpDemo.Helpers;
using static CSharpDemo.Demos.Extensions.ExtensionDemo;

namespace CSharpDemo.Demos.Extensions
{
    public static class OrderExtensions
    {
        public static string GetItems(this Order order)
        {
            return string.Join(", ", order.Items);
        }

        public static void PrintOrder(this Order order, string header)
        {
            Console.WriteLine($"{header}");
            Console.WriteLine(order.GetItems());
        }
    }

    public class ExtensionDemo : DemoRunner<ExtensionDemo>
    {
        public class Order
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<string> Items { get; set; }

            public void PrintOrder(string caption)
            {
                Console.WriteLine($"{caption}");
                Console.WriteLine(this.GetItems());
            }
        }

        [DemoCaption("Extension method demo")]
        public void Demo1()
        {
            Order order = new Order() { Items = new List<string> { "Tea", "Coffee" } };

            Console.WriteLine(order.GetItems());
        }

        [DemoCaption("Extension method have the lowest priority")]
        public void Demo2()
        {
            Order order = new Order() { Items = new List<string> { "Tea", "Coffee" } };

            // Method from the class will be invoked
            order.PrintOrder("New order (class method)");

            // Extension method will be invoked
            order.PrintOrder(header: "New order (extension method)");
        }
    }
}
