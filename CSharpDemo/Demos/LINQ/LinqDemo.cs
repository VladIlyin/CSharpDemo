using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.LINQ;

public class LinqDemo : DemoRunner<LinqDemo>
{
    [DemoCaption("Count extension method")]
    public void Demo11()
    {
        var list = new List<int>() { 0, 0, 0, 1 };

        var filtered = list.Where(w => w == 0);

        for (var i = 0; i < filtered.Count(); i++)
        {
            list.Remove(0);
        }

        // 0, 1
        ConsoleHelper.WriteCollection(list);
    }

    [DemoCaption("GroupBy extension method - list of int")]
    public void Demo5()
    {
        var list = new List<int>() { 0, 1, 0, 1, 0, 0, 1 };

        var groupedList = list
            .GroupBy(x => x)
            .Select(x => new { x.Key, Count = x.Count() })
            .ToList();

        // { Key = 0, Count = 4 }
        // { Key = 1, Count = 3 }
        ConsoleHelper.WriteLineCollection(groupedList);
    }

    [DemoCaption("GroupBy extension method - list of int")]
    public void Demo6()
    {
        var list = new List<int>() { 0, 1, 0, 1, 0, 0, 1 };

        var groupedList = list
            .GroupBy(x => x)
            .Select(x => new { x.Key, Count = x.Count() })
            .ToList();

        // { Key = 0, Count = 4 }
        // { Key = 1, Count = 3 }
        ConsoleHelper.WriteLineCollection(groupedList);
    }
        
    [DemoCaption("Select many")]
    public void Demo7()
    {
        List<string> phrases = new() { "an apple a day", "the quick brown fox" };

        var query = phrases.SelectMany(x => x.Split(" "));

        // an apple a day the quick brown fox
        ConsoleHelper.WriteCollection(query);

        List<IList<string>> strMatrix = new()
        {
            new List<string>() { "a", "b" },
            new List<string>() { "c", "d" },
            new List<string>() { "e", "f" },
        };

        // a b c d e f
        ConsoleHelper.WriteCollection(strMatrix.SelectMany(x => x));
    }
}