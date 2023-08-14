using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.AsyncAwait.AwaitableAwaiter
{
    public static class AwaitableAwaiterDemo
    {
        public static async Task Run()
        {
            // awaitable-awaiter pattern
            // https://weblogs.asp.net/dixin/understanding-c-sharp-async-await-2-awaitable-awaiter-pattern
            int resultFuncAwaitable = await new FuncAwaitable<int>(() => 1000);
            int resultTask = await Task.Run(() => 1000);

            Console.WriteLine(resultFuncAwaitable);
            Console.WriteLine(resultTask);

            // another example of awaitable object implenting awaitable-awaiter pattern
            var yieldAwaitable = Task.Yield();
        }
    }
}
