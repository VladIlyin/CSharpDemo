using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSharpDemo.DemoRunner;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CSharpDemo.Demos.Records;

public class RecordDemo : DemoRunner<RecordDemo>
{
    record Person(string name, int age);

    record struct Point(int x, int y);
    record struct Rectangle(int w, int h);
    readonly record struct ImmutablePoint(int x, int y);

    [DemoCaption("Record: create record instance")]
    public void Demo1()
    {
        var pers1 = new Person("Dave", 10);
        Console.WriteLine(pers1);

        // we can't change record property (it's init-only)
        // pers1.age = 10;

        // No default constructor available
        // var pers2 = new Person();
    }

    record Car(
        [property: JsonPropertyName("carModel")] string model,
        [property: JsonPropertyName("carColor")] string color,
        int year,
        int mileage);

    [DemoCaption("Record: adding JsonPropertyName")]
    public void Demo2()
    {
        var car = new Car("BMW", "red", 2009, 50_000);
        var carSerialized = JsonConvert.SerializeObject(car);

        // JsonSerializer.Serialize();

        Console.WriteLine(carSerialized);
    }

    [DemoCaption("Record struct")]
    public void Demo3()
    {
        var p = new Point(1, 1);

        Console.WriteLine(p);

        // record struct is not immutable
        p.x = 2;
        p.y = 2;

        Console.WriteLine(p);
    }

    [DemoCaption("Record struct: value-based equality")]
    public void Demo31()
    {
        var p1 = new Point(1, 1);
        var p2 = new Point(1, 1);
        var p3 = new Point(2, 2);

        Console.WriteLine($"p1 {p1}");
        Console.WriteLine($"p2 {p1}");
        Console.WriteLine($"p3 {p1}");
        Console.WriteLine($"p1 equals p2 {p1 == p2}"); // true
        Console.WriteLine($"p2 equals p3 {p2 == p3}"); // false
    }
        
    [DemoCaption("Record struct: deconstruction")]
    public void Demo32()
    {
        var rect = new Rectangle(100, 200);

        // Deconstruction
        (var width, var height) = rect;
        Console.WriteLine($"Width: {width}, Height: {height}");
    }

    [DemoCaption("Readonly record struct: immutable")]
    public void Demo4()
    {
        var p = new ImmutablePoint(1, 1);

        Console.WriteLine(p);

        // record struct is immutable
        // p.x = 2;
        // p.y = 2;
    }
}