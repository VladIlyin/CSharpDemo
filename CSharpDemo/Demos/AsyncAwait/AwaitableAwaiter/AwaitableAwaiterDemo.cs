namespace CSharpDemo.Demos.AsyncAwait.AwaitableAwaiter;

public static class AwaitableAwaiterDemo
{
    public static async Task Run()
    {
        // awaitable-awaiter pattern
        // https://weblogs.asp.net/dixin/understanding-c-sharp-async-await-2-awaitable-awaiter-pattern
        var resultFuncAwaitable = await new FuncAwaitable<int>(() => 1000);
        var resultTask = await Task.Run(() => 1000);

        Console.WriteLine(resultFuncAwaitable);
        Console.WriteLine(resultTask);

        // another example of awaitable object implenting awaitable-awaiter pattern
        var yieldAwaitable = Task.Yield();
    }
}