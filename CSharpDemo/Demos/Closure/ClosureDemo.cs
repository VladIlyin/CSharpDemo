using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Closure;

public class ClosureDemo : DemoRunner<ClosureDemo>
{
    [DemoCaption("Closure: 1st class function + free variable")]
    public void Demo1()
    {
        var counter = getCounter(0);

        Console.WriteLine(counter());   // 1
        Console.WriteLine(counter());   // 2
        Console.WriteLine(counter());   // 3

        Func<int> getCounter(int seed)
        {
            var counter = 0;

            var func = () => ++counter;
            return func;
        }
    }

    delegate void PrintNumber();

    [DemoCaption("Closure: classic issue with closure")]
    public void Demo2()
    {
        var actions = new List<Action>();

        for (var i = 0; i < 10; i++)
        {
            // i is a class field that increments inside this loop
            actions.Add(() => Console.Write($"{i} "));
        }

        foreach (var action in actions)
        {
            action();
        }

        /*
         * Output:
         * 10 10 10 10 10 10 10 10 10 10
         */
    }

    /* sharplab.io
     *
     * private sealed class <>c__DisplayClass1_0
     *  {
     *      public int i;
     *
     *      internal void <Demo1>b__0()
     *      {
     *          Console.WriteLine(i);
     *      }
     *  }
     *
     *  public void Demo1()
     *  {
     *      List<Action> list = new List<Action>();
     *      <>c__DisplayClass1_0 <>c__DisplayClass1_ = new <>c__DisplayClass1_0();
     *      <>c__DisplayClass1_.i = 0;
     *      while (<>c__DisplayClass1_.i < 10)
     *      {
     *          list.Add(new Action(<>c__DisplayClass1_.<Demo1>b__0));
     *          <>c__DisplayClass1_.i++;
     *      }
     *
     *      ...
     *  }
     */
}