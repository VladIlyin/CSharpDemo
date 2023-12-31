﻿namespace CSharpDemo.Demos.AsyncAwait.AwaitableAwaiter;

public class FuncAwaitable<TResult>: IAwaitable<TResult>
{
    private readonly Func<TResult> _func;

    public FuncAwaitable(Func<TResult> func)
    {
        _func = func;
    }

    public FuncAwaiter<TResult> GetAwaiter()
    {
        return new FuncAwaiter<TResult>(_func);
    }
}