using CSharpDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.Demos.Closure
{
    public class ClosureDemo : DemoRunner<ClosureDemo>
    {
        delegate void PrintNumber();

        public void Demo1()
        {
            List<Action> actions = new List<Action>();

            for (var i = 0; i < 10; i++)
            {
                // int num = i;
                actions.Add(() => Console.WriteLine(i));
            }

            foreach (var action in actions)
            {
                action();
            }
        }
    }
}
