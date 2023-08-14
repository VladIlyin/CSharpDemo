using CSharpDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.Demos.AsyncAwait
{
    public class AsyncAwaitDemo : DemoRunner<AsyncAwaitDemo>
    {
        private static string result;

        public void Demo10()
        {
            SaySomething();
            Console.WriteLine(result); // null, cause SaySomething is not awaited

            async Task<string> SaySomething()
            {
                await Task.Delay(5);
                result = "Hello world!";
                return "Something";
            }
        }
    }
}
