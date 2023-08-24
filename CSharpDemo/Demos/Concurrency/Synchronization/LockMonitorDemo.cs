using System.Diagnostics.Tracing;
using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Concurrency.Synchronization
{
    public partial class SynchronizationPrimitivesDemo : DemoRunner<SynchronizationPrimitivesDemo>
    {
        private object objLock = new();
        private int counter;

        [DemoCaption("Lock")]
        public async Task Demo1()
        {
            void IncrementingCounter()
            {
                lock (objLock)
                {
                    // error Cannot await in the body of a lock statement
                    // await Task.Delay(100);
                    counter++;
                }
            }

            var tasks = new List<Action>();

            for (var i = 0; i < 1000; i++)
            {
                tasks.Add(IncrementingCounter);
            }

            Parallel.Invoke(tasks.ToArray());

            Console.WriteLine(counter); // 1000
        }

        [DemoCaption("Await inside the Monitor")]
        public async Task Demo2()
        {
            var counter = 0;

            var tasks = new List<Task>();

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(DoWork());
            }

            /*
             System.Threading.SynchronizationLockException:
             "Object synchronization method was called  
             from an unsynchronized block of code
            */
            try
            {
                await Task.WhenAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            async Task DoWork()
            {
                Monitor.Enter(objLock);

                try
                {
                    var n = await GetNumber();
                    counter += n;
                    Console.WriteLine(counter);
                }
                finally
                {
                    Monitor.Exit(objLock);
                }
            }

            async Task<int> GetNumber()
            {
                await Task.Yield();
                return await Task.FromResult(1);
            }
        }
    }
}
