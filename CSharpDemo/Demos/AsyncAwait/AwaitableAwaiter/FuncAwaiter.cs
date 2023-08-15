using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.AsyncAwait.AwaitableAwaiter
{
    public readonly struct FuncAwaiter<TResult> : IAwaiter<TResult>
    {
        private readonly Task<TResult> task;

        public FuncAwaiter(Func<TResult> function)
        {
            this.task = new Task<TResult>(function);
            this.task.Start();
        }

        public bool IsCompleted => task.IsCompleted;

        public TResult GetResult()
        {
            return task.GetAwaiter().GetResult();
        }

        public void OnCompleted(Action continuation)
        {
            new Task(continuation).Start();
        }
    }
}
