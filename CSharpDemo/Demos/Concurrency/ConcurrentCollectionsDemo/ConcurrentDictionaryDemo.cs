using System.Collections.Concurrent;
using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Concurrency.ConcurrentCollectionsDemo;

public class ConcurrentDictionaryDemo : DemoRunner<ConcurrentDictionaryDemo>
{
    [DemoCaption("Concurrent Dictionary Demo - AddOrUpdate")]
    public void Demo1()
    {
        var dict = new ConcurrentDictionary<int, int>();

        // Bombard the ConcurrentDictionary with 1000 competing AddOrUpdates
        Parallel.For(0, 1000, i =>
        {
            dict.AddOrUpdate(1, 1, (key, value) => ++value);
        });

        Console.WriteLine("After 1000 AddOrUpdates, dict[1] = {0}, should be 1000", dict[1]);
    }

    [DemoCaption("Concurrent Dictionary Demo - GetOrAdd")]
    public void Demo2()
    {
        var dict = new ConcurrentDictionary<int, int>();

        dict.GetOrAdd(0, 0);
        dict.GetOrAdd(1, static (key) => 1);
        dict.GetOrAdd(2, static (key, arg) => arg, 2);

        Console.WriteLine("dict[0] = {0}", dict[0]); // 0
        Console.WriteLine("dict[1] = {0}", dict[1]); // 1
        Console.WriteLine("dict[2] = {0}", dict[2]); // 2

        dict.GetOrAdd(0, 111); // key already exists

        Console.WriteLine("dict[0] = {0}", dict[0]); // 0
    }

    [DemoCaption("Concurrent Dictionary Demo - TraAdd TryUpdate TryRemove")]
    public void Demo3()
    {
        var dict = new ConcurrentDictionary<int, string>();
        var numFailures = 0;

        if (!dict.TryAdd(1, "one"))
        {
            Console.WriteLine("TryAdd failed");
            numFailures++;
        }

        if (dict.TryAdd(1, "uno"))
        {
            Console.WriteLine("TryAdd succeeded but should have failed");
            numFailures++;
        }

        if (!dict.TryUpdate(1, "uno", "one"))
        {
            Console.WriteLine("TryUpdate failed but should have succeeded");
            numFailures++;
        }

        if (dict.TryUpdate(2, "uno", "one"))
        {
            Console.WriteLine("TryUpdate succeeded but should have failed");
            numFailures++;
        }

        if (!dict.TryRemove(1, out var value1))
        {
            Console.WriteLine("TryRemove failed but should have succeeded");
            numFailures++;
        }

        if (numFailures == 0) Console.WriteLine("OK!");
    }
}