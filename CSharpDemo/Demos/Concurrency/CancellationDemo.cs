using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Concurrency
{
    public class CancellationDemo : DemoRunner<CancellationDemo>
    {
        [DemoCaption("CancellationTokenSource Demo")]
        public async Task Demo1()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            var t1 = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000, cts.Token);
                    Console.WriteLine("Performing task 1...");
                }
            }, cts.Token);


            var t2 = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000, cts.Token);
                    Console.WriteLine("Performing task 2...");
                }
            }, cts.Token);

            try
            {
                await Task.WhenAll(t1, t2);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// The polling technique in this recipe
        /// should only be used if you have a processing loop that needs to support
        /// cancellation.
        /// </summary>
        /// <returns></returns>
        [DemoCaption("Loop cancellation")]
        public async Task Demo2()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Loop {0}", i);
                    token.ThrowIfCancellationRequested();
                }
            });

            Console.WriteLine("Press esc to cancel, or any key to proceed\n");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                cts.Cancel();
            }

            try
            {
                await t;
                Console.WriteLine("\nLoop completed successfully");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nOperation was cancelled");
            }
        }
    }
}
