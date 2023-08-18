using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Closure
{
    public class ClosureDemo : DemoRunner<ClosureDemo>
    {
        delegate void PrintNumber();

        [DemoCaption("Closure: classic issue with closure")]
        public void Demo1()
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

        [DemoCaption("Closure: classic issue - solution")]
        public void Demo2()
        {
            var actions = new List<Action>();

            for (var i = 0; i < 10; i++)
            {
                var num = i;
                actions.Add(() => Console.Write($"{num} "));
            }

            foreach (var action in actions)
            {
                action();
            }

            /*
             * Output:
             * 0 1 2 3 4 5 6 7 8 9
             */
        }
    }
}
