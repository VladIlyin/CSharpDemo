using CSharpDemo.DemoRunner;
using System.Collections.Specialized;

namespace CSharpDemo.Demos.Collections;

public class DictionaryDemo : DemoRunner<DictionaryDemo>
{
    [DemoCaption("Ordered Dictionary")]
    public void Demo2()
    {
        var dictionary = new Dictionary<int, string>
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" }
        };

        var orderedDictionary = new OrderedDictionary
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" }
        };

        dictionary.Remove(3);
        dictionary.Add(6, "Six");
        dictionary.Remove(5);
        dictionary.Add(7, "Seven");
        dictionary.Remove(1);

        orderedDictionary.Remove(3);
        orderedDictionary.Add(6, "Six");
        orderedDictionary.Remove(5);
        orderedDictionary.Add(7, "Seven");
        orderedDictionary.Remove(1);

        foreach (var item in dictionary)
        {
            Console.WriteLine($"{item}");
        }

        Console.WriteLine();

        foreach (var item in orderedDictionary)
        {
            Console.WriteLine($"{item}");
        }
    }

    [DemoCaption("Sorted Dictionary")]
    public void Demo3()
    {
        var dictionary = new SortedDictionary<int, string>
        {
            { 3, "Three" },
            { 12, "Twenty" },
            { 1, "One" },
            { 5, "Five" }
        };

        foreach (var item in dictionary)
        {
            Console.WriteLine($"{item.Key} {item.Value}");
        }
    }

    [DemoCaption("Simple List")]
    public void Demo4()
    {
        var list = new List<int>
        {
            5, 2, 1, 4, 3,
        };

        foreach (var item in list)
        {
            Console.WriteLine($"{item}");
        }

        // System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
        // It's not allowed to remove items from List while enumerating items

        //foreach (var item in list)
        //{
        //    list.RemoveAt(0);
        //}
    }
}