using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Concurrency.Synchronization
{
    public partial class SynchronizationPrimitivesDemo : DemoRunner<SynchronizationPrimitivesDemo>
    {
        [DemoCaption("Lock")]
        public async Task Demo1()
        {
            object objLock = new();
            var counter = 0;

            void IncrementingCounter()
            {
                lock (objLock)
                {
                    counter++;
                }
            }

            IEnumerable<Action> GetTasks()
            {
                for (var i = 0; i < 1000; i++)
                {
                    yield return IncrementingCounter;
                }
            }

            Parallel.Invoke(GetTasks().ToArray());

            Console.WriteLine(counter); // 1000
        }

        [DemoCaption("Await inside the Monitor")]
        public async Task Demo2()
        {
            object objLock = new();

            var lockTaken = false;

            try
            {
                Monitor.Enter(objLock, ref lockTaken);

                Console.WriteLine($"lock 1 is taken: {lockTaken}");
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId); // some id1
                
                // comment this line out to make it works properly
                await Task.Yield();

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId); // some id2 differs form id1

                // omit try finally for simplicity
                var lockTaken2 = false;
                Monitor.TryEnter(objLock, 1000, ref lockTaken2);

                Console.WriteLine($"lock 2 is taken: {lockTaken2}");

                if (lockTaken2)
                {
                    Monitor.Exit(objLock);
                    Console.WriteLine("Exit lock 2");
                }
            }
            finally
            {
                if (lockTaken)
                {
                    // Exception throws here:
                    // Object synchronization method was called from an unsynchronized block of code
                    try
                    {
                        Monitor.Exit(objLock);
                        Console.WriteLine("Exit lock 1");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
