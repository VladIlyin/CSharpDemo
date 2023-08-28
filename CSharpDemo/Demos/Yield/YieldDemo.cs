using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Yield;

public class YieldDemo : DemoRunner<YieldDemo>
{
    [DemoCaption("Yield - fibonacci numbers")]
    public void Demo1()
    {
        foreach (var num in GetFibonacci(1000))
        {
            Console.WriteLine(num);
        }

        IEnumerable<int> GetFibonacci(int maxValue)
        {
            var previous = 0;
            var current = 1;

            while (current <= maxValue)
            {
                yield return current;

                var newCurrent = previous + current;
                previous = current;
                current = newCurrent;
            }
        }
    }

    [DemoCaption("Yield - from file")]
    public void Demo2()
    {
        foreach (var line in GetLines(@"Demos\Yield\sometext.txt"))
        {
            Console.WriteLine(line);
        }

        IEnumerable<string> GetLines(string path)
        {
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
                yield return reader.ReadLine();
        }
    }
}