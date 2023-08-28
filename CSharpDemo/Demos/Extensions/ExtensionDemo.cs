using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Extensions;

/*
 * Extension methods enable you to "add" methods
 * to existing types without creating a new derived type,
 * recompiling, or otherwise modifying the original type.
 */
public static class IntExtension
{
    public static int Plus(this int a, int b)
    {
        return a + b;
    }

    public static int Pow(this int a, int power)
    {
        return (int)Math.Pow(a, power);
    }
}

public class Order
{
    public List<string> Items { get; set; }

    public void PrintOrder()
    {
        Console.WriteLine("I'm class method");
    }

    public void AddItem(string item)
    {
        Items.Add(item);
    }
}

public static class OrderExtensions
{
    public static string GetItems(this Order order)
    {
        return string.Join(", ", order.Items);
    }

    public static void AddItem(this Order order, string name)
    {
        order.Items.Add(name);
    }

    // never called
    public static void PrintOrder(this Order order)
    {
        Console.WriteLine("I'm extension method");
    }
}

public class ExtensionDemo : DemoRunner<ExtensionDemo>
{
    [DemoCaption("Extension method for int")]
    public void Demo1()
    {
        var a = 5;
        var b = 5;

        // 10
        Console.WriteLine(a.Plus(b));

        // 50
        Console.WriteLine(a.Pow(2) + b.Pow(2));
    }


    [DemoCaption("Extension method for Order class")]
    public void Demo2()
    {
        var order = new Order() { Items = new List<string> { "Tea", "Coffee" } };

        Console.WriteLine(order.GetItems());
    }

    [DemoCaption("Extension method have the lowest priority")]
    public void Demo3()
    {
        var order = new Order() { Items = new List<string> { "Tea", "Coffee" } };

        /*
         * At compile time, extension methods always have
         * lower priority than instance methods defined in the type itself
         */

        // I'm class method
        order.PrintOrder();

        order.AddItem("Pizza");         // class method is called
        order.AddItem(name: "Pizza");   // extension method is called
    }
}