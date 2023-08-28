using System.Runtime.InteropServices;
using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Span;

public class SpanDemo : DemoRunner<SpanDemo>
{
    [DemoCaption("Span Demo: create a span over an array")]
    public void Demo1()
    {
        var arr = new int[] { 10, 20, 30, 40, 50, 60, 70 };
        var span = new Span<int>(arr);

        // change span values
        for (var i = 0; i < span.Length; i++)
        {
            span[i]++;
        }

        ConsoleHelper.WriteLineCollection(arr); // 11 21 31 41 51 61 71
    }

    [DemoCaption("Span Demo: create a span from naive memory")]
    public void Demo2()
    {
        var native = Marshal.AllocHGlobal(100);
        Span<byte> nativeSpan;

        unsafe
        {
            nativeSpan = new Span<byte>(native.ToPointer(), 100);
        }

        byte data = 0;
        for (var ctr = 0; ctr < nativeSpan.Length; ctr++)
            nativeSpan[ctr] = data++;

        var nativeSum = 0;
        foreach (var value in nativeSpan)
            nativeSum += value;

        Console.WriteLine($"The sum is {nativeSum}"); // 4950
        Marshal.FreeHGlobal(native);
    }

    [DemoCaption("Span Demo: create a span on the stack")]
    public void Demo3()
    {
        byte data = 0;
        Span<byte> stackSpan = stackalloc byte[100];
        for (var ctr = 0; ctr < stackSpan.Length; ctr++)
            stackSpan[ctr] = data++;

        var stackSum = 0;
        foreach (var value in stackSpan)
            stackSum += value;

        Console.WriteLine($"The sum is {stackSum}");
    }

    [DemoCaption("Span Demo: slice of an array.")]
    public void Demo4()
    {
        var arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            
        // create slice of an array using Span
        var slice = new Span<int>(arr, 2, 3);

        ConsoleHelper.WriteLineSpan(slice, "Created with Span constructor"); // 3 4 5

        // or use range
        slice = arr[2..5];

        ConsoleHelper.WriteLineSpan(slice, "Created with range"); // 3 4 5
    }

    [DemoCaption("Span Demo: binary search.")]
    public void Demo5()
    {
        var arr = new int[] { 123, 214, 3148, 4748, 555, 678, 79874 };

        var slice = new Span<int>(arr);

        Console.WriteLine(slice.BinarySearch(3148)); // 2
        Console.WriteLine(slice.BinarySearch(111)); // less han zero
    }

    [DemoCaption("Span Demo: memory overlapping.")]
    public void Demo6()
    {
        var arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };

        Span<int> slice = new(arr, 0, 4);
        ReadOnlySpan<int> readOnlySpan = new(arr, 3, 4);

        var isOverlapped = slice.Overlaps(readOnlySpan);

        Console.WriteLine(isOverlapped); // true

        slice = new(arr, 0, 3);
        readOnlySpan = new(arr, 3, 4);

        isOverlapped = slice.Overlaps(readOnlySpan);

        Console.WriteLine(isOverlapped); // false
    }

    [DemoCaption("Span Demo: range syntax vs span")]
    public void Demo7()
    {
        var arr = Enumerable.Range(1, 5).ToArray();
            
        var copy = arr[0..5];
        Span<int> span = new(arr, 0, 4);
            
        // It won't change the original array, we only change copy of the array
        copy[0] = 111;

        ConsoleHelper.WriteCollection(arr); // 1, 2, 3, 4, 5

        // It will change the original array, cause of using Span<T>
        span[0] = 111;

        ConsoleHelper.WriteCollection(arr); // 111, 2, 3, 4, 5
    }

    [DemoCaption("Span Demo: span with range syntax")]
    public void Demo8()
    {
        var arr = Enumerable.Range(1, 5).ToArray();

        var copy = arr[0..5];
        Span<int> span = new(arr, 0, 4);

        // It won't change the original array, we only change copy of the array
        copy[0] = 111;

        ConsoleHelper.WriteCollection(arr); // 1, 2, 3, 4, 5

        // It will change the original array, cause of using Span<T>
        span[0] = 111;

        ConsoleHelper.WriteCollection(arr); // 111, 2, 3, 4, 5
    }

    record Order(int Id);

    public void Demo9()
    {
        var orders = new Order[5] { new(1), new(2), new(3), new(4), new(5) };
        Span<Order> span = new(orders);

        Span<Order> slice1 = span[..^3];
        Span<Order> slice2 = span[^3..];

        ConsoleHelper.WriteSpan(slice1);
        ConsoleHelper.WriteSpan(slice2);
    }

    [DemoCaption("Read-only span demo: string to ReadOnlySpan<char>")]
    public void Demo10()
    {
        Span<char> span = "03 06 2019".ToCharArray();

        var spanDay = span[..2];
        var spanMonth = span[3..5];
        var spanYear = span[6..];

        var dte = new DateTime(
            Convert.ToInt32(spanYear.ToString()),
            Convert.ToInt32(spanMonth.ToString()),
            Convert.ToInt32(spanDay.ToString()));

        Console.WriteLine(dte);
    }

    [DemoCaption("Span Demo: unsafe changing of a string with span<char> (char*)")]
    public void Demo11()
    {
        var str = "Sample string";

        unsafe
        {
            fixed (char* p = str)
            {
                Span<char> span = new(p, str.Length);
                span[0] = 'X';
            }
        }

        Console.WriteLine(str);             // Xample string

        // but now, oops...
        Console.WriteLine("Sample string"); // Xample string
    }
}