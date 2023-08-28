using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Collections;

public class ImmutableCollectionsDemo : DemoRunner<ImmutableCollectionsDemo>
{
    [DemoCaption("Immutable List")]
    public void Demo1()
    {
        var list = ImmutableList<int>.Empty;

        list = list.Insert(0, 13);
        list = list.Insert(0, 7);
        list = list.Add(15);
            
        // Displays 7 13 15.
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }

    [DemoCaption("Immutability Sets")]
    public void Demo2()
    {
        var immutableSet = ImmutableHashSet<int>.Empty;

        immutableSet = immutableSet.Add(13);
        immutableSet = immutableSet.Add(7);
        immutableSet = immutableSet.Add(17);

        // Displays 7 13 17
        foreach (var item in immutableSet)
        {
            Console.WriteLine(item);
        }

        immutableSet = immutableSet.Remove(7);

        var sortedSet = ImmutableSortedSet<int>.Empty;
            
        sortedSet = sortedSet.Add(13);
        sortedSet = sortedSet.Add(7);
        sortedSet = sortedSet.Add(17);

        // Displays 7 13 17
        foreach (var item in sortedSet)
        {
            Console.WriteLine(item);
        }

        var smallestItem = sortedSet[0];

        Console.WriteLine(smallestItem); // 7
    }

    [DemoCaption("Immutability Dictionary")]
    public void Demo3()
    {
        ImmutableDictionary<int, string> dictionary =
            ImmutableDictionary<int, string>.Empty;

        dictionary = dictionary.Add(10, "Ten");
        dictionary = dictionary.Add(21, "Twenty-One");
        dictionary = dictionary.SetItem(10, "Diez");
        dictionary = dictionary.Add(1, "One");

        // Displays "10Diez" followed by "21Twenty-One".
        foreach (KeyValuePair<int, string> item in dictionary)
        {
            Console.WriteLine(item.Key + item.Value);
        }

        ImmutableSortedDictionary<int, string> sortedDictionary =
            ImmutableSortedDictionary<int, string>.Empty;
        sortedDictionary = sortedDictionary.Add(10, "Ten");
        sortedDictionary = sortedDictionary.Add(21, "Twenty-One");
        sortedDictionary = sortedDictionary.SetItem(10, "Diez");
        sortedDictionary = sortedDictionary.Add(1, "One");

        // Displays "10Diez" followed by "21Twenty-One".
        foreach (KeyValuePair<int, string> item in sortedDictionary)
        {
            Console.WriteLine(item.Key + item.Value);
        }
    }
}