using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.GetHashCode;

public struct Point
{
    public int X;
    public int Y;
}

public class Point3D
{
    public int X;
    public int Y;
    public int Z;
}

public record ColorPoint
{
    public int X;
    public int Y;
    public string Color;
}

public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        var other = obj as Person;
        if (other == null) return false;

        return Equals(this.Name, other.Name)
               && this.Age == other.Age;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }
}

public class GetHashCodeDemo : DemoRunner<GetHashCodeDemo>
{
    [DemoCaption("GetHashCode: struct")]
    public void Demo1()
    {
        var p1 = new Point() { X = 1, Y = 1 };
        var p2 = new Point() { X = 1, Y = 1 };

        // True
        Console.WriteLine(
            $"p1.GetHashCode() == p2.GetHashCode() is {p1.GetHashCode() == p2.GetHashCode()}"); // True

    }
        
    [DemoCaption("GetHashCode: class")]
    public void Demo2()
    {
        var p13d = new Point3D() { X = 1, Y = 1, Z = 1 };
        var p23d = new Point3D() { X = 1, Y = 1, Z = 1 };

        // False
        Console.WriteLine(
            $"p13d.GetHashCode() == p23d.GetHashCode() is {p13d.GetHashCode() == p23d.GetHashCode()}");
    }

    [DemoCaption("GetHashCode: Record")]
    public void Demo3()
    {
        var cp1 = new ColorPoint() { X = 1, Y = 1, Color = "Red" };
        var cp2 = new ColorPoint() { X = 1, Y = 1, Color = "Red" };

        // True
        Console.WriteLine($"cp1.GetHashCode() == cp2.GetHashCode() is { cp1.GetHashCode() == cp2.GetHashCode() }");

        // In record method GetHashCode is already implemented
        // We can't add two identical ColorPoint object
        Dictionary<ColorPoint, string> dict2 = new()
        {
            { new() { X = 1, Y = 1, Color = "Red" }, "(1, 1, Red)" },
            // error { new() { X = 1, Y = 1, Color = "Red" }, "(1, 1, Red)" },
        };
        Console.WriteLine(dict2.Count); // 1

        // It's ok two add two identical Point3D object, cause its just old plain class
        Dictionary<Point3D, string> dict = new()
        {
            { new() { X = 1, Y = 1, Z = 1 }, "(1, 1, 1)" },
            { new() { X = 1, Y = 1, Z = 1 }, "(1, 1, 1)" },
        };
        Console.WriteLine(dict.Count); // 2
    }

    [DemoCaption("GetHashCode: class with GetHashCode implemented, adding to Dictionary/HashSet")]
    public void Demo4()
    {
        Person p1 = new() { Age = 30, Name = "Jake" };
        Person p2 = new() { Age = 30, Name = "Jake" };
        Person p3 = new() { Age = 25, Name = "Bob" };

        HashSet<Person> set = new()
        {
            p1,
            p2,
            p2
        };

        Console.WriteLine($"HashSet has {set.Count} items"); // 2

        Dictionary<Person, int> dict = new();

        dict.TryAdd(p1, 1);
        dict.TryAdd(p2, 2); // false cause p1 equals p2
        dict.TryAdd(p2, 3); 

        Console.WriteLine($"Dictionary has {set.Count} items"); // 2
    }


    [DemoCaption("GetHashCode: Tuples")]
    public void Demo5()
    {
        var tuple1 = (0, 1);
        var tuple2 = (0, 1);

        Console.WriteLine($"tuple 1 {tuple1}");
        Console.WriteLine($"tuple 1 {tuple2}");

        // Identical hash code for both tuples
        // True
        Console.WriteLine(
            $"tuple1.GetHashCode() == tuple2.GetHashCode() is {tuple1.GetHashCode() == tuple2.GetHashCode()}");
            

        Dictionary<(int, int), string> dictTupleKey = new();

        dictTupleKey.TryAdd((0, 1), "01");
        dictTupleKey.TryAdd((0, 1), "01");

        Console.WriteLine(dictTupleKey.Count); // 1
    }

    [DemoCaption("GetHashCode: Thread")]
    public void Demo6()
    {
        var th = new Thread(() => Console.WriteLine("Thread"), 1000);

        // ManagedThreadId == HashCode
        Console.WriteLine($"Thread: ManagedThreadId {th.ManagedThreadId}, GetHashCode {th.GetHashCode()}");
    }
}