namespace CSharpDemo.Demos.AsyncAwait.AwaitableAwaiter;

public interface IAwaitable<TResult>
{
    public FuncAwaiter<TResult> GetAwaiter();
}