using System.Runtime.CompilerServices;

namespace CSharpDemo.AsyncAwait.AwaitableAwaiter
{
    public interface IAwaiter<TResult> : INotifyCompletion
    {
        bool IsCompleted { get; }

        TResult GetResult();
    }
}
