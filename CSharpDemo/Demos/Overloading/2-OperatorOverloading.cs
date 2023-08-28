using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Overloading;

public partial class OverloadingDemo
{
    public class Order
    {
        public string Name { get; set; }

        public List<string> Items { get; set; }

        public decimal Total { get; set; }

        public static OrdersTotal operator +(Order ord1, Order ord2)
        {
            return new OrdersTotal()
            {
                Total = ord1.Total + ord2.Total,
                Orders = new List<string> { ord1.Name, ord2.Name }
            };
        }

        public static bool operator >(Order ord1, Order ord2)
        {
            return ord1.Total > ord2.Total;
        }

        public static bool operator <(Order ord1, Order ord2)
        {
            return ord1.Total < ord2.Total;
        }

        public static implicit operator decimal(Order order)
        {
            return order.Total;
        }

        public static explicit operator Order(List<string> items)
        {
            return new Order() { Items = items };
        }

        public static implicit operator Order(string s)
        {
            return new Order { Name = s };
        }

        /* Error: at least one parameter must be Order
        public static Order operator +(IList<Order> left, IList<Order> right)
        {
           
        }
        */
    }

    public class OrdersTotal
    {
        public List<string> Orders { get; set; }
        public decimal Total { get; set; }

        public static OrdersTotal operator +(OrdersTotal ordersTotal, Order ord)
        {
            ordersTotal.Orders.Add(ord.Name);
            ordersTotal.Total = ordersTotal.Total + ord.Total;
            return ordersTotal;
        }
    }

    [DemoCaption("OVerloading: Operator + overloading: sum of three Order class")]
    public void Demo2()
    {
        var ord1 = new Order() { Name = "First order", Total = 100 };
        var ord2 = new Order() { Name = "Second order", Total = 120 };
        var ord3 = new Order() { Name = "Third order", Total = 50 };

        var allOrders = ord1 + ord2 + ord3;

        Console.WriteLine("Total: {0}", allOrders.Total); // 270
    }

    [DemoCaption("Operators > < overloading: comparison of two Order class")]
    public void Demo3()
    {
        var ord1 = new Order() { Name = "First order", Total = 100 };
        var ord2 = new Order() { Name = "Second order", Total = 200 };
        var ord3 = new Order() { Name = "Third order", Total = 300 };

        var allOrders = ord1 + ord2 + ord3;

        Console.WriteLine("Ord1 > Ord2: {0}", ord1 > ord2); // false
        Console.WriteLine("Ord2 < Ord3: {0}", ord2 < ord3); // true
    }

    [DemoCaption("Implicit conversion: Order class to decimal")]
    public void Demo4()
    {
        var ord = new Order() { Name = "First order", Total = 123.50M };

        decimal total = ord;

        Console.WriteLine(total); // 123,50

        // Side-effect of implicit operator - now Order converts to decimal implicitly
        Console.WriteLine(ord); // 123,50
    }

    [DemoCaption("Explicit conversion: List<string> to Order class")]
    public void Demo5()
    {
        var ord = (Order)new List<string> { "Apple", "Tomato", "Banana", "Bread", "Butter" };

        ConsoleHelper.WriteLineCollection(ord.Items);
    }

    [DemoCaption("Implicit conversion: string to order")]
    public void Demo6()
    {
        Order ord = "Apple";

        ConsoleHelper.WriteLineCollection(ord.Name); // Apple
    }
}