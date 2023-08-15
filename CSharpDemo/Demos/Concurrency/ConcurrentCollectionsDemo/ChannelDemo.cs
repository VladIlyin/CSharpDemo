using Bogus.Bson;
using CSharpDemo.Helpers;
using Newtonsoft.Json.Linq;
using System.Threading.Channels;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpDemo.Demos.Concurrency.ConcurrentCollectionsDemo
{
    /// <summary>
    /// Channels are an implementation of the producer/consumer conceptual programming model.
    /// In this programming model, producers asynchronously produce data,
    /// and consumers asynchronously consume that data.
    /// In other words, this model hands off data from one party to another.
    /// </summary>
    public class ChannelDemo : DemoRunner<ChannelDemo>
    {
        [DemoCaption("Channel - Throttling queue with capacity = 3")]
        public async Task Demo1()
        {
            var queue = Channel.CreateBounded<int>(3);
            var writer = queue.Writer;
            var reader = queue.Reader;

            // Read
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);

                await foreach (var item in reader.ReadAllAsync())
                {
                    ConsoleHelper.WriteLine($"Read: \t{item}", ConsoleColor.Green);
                    await Task.Delay(500);
                }
            });

            // Write
            for (var i = 0; i < 10; i++)
            {
                await writer.WriteAsync(i);
                ConsoleHelper.WriteLine($"Write: \t{i}", ConsoleColor.Yellow);
            }

            writer.Complete();

            Console.WriteLine("Writer complete");
        }

        [DemoCaption("Channel - Sampling queue with capacity = 10, DropOldest mode")]
        public async Task Demo2()
        {
            var queue = Channel.CreateBounded<int>(
                new BoundedChannelOptions(5)
                {
                    FullMode = BoundedChannelFullMode.DropOldest,
                });

            var writer = queue.Writer;
            var reader = queue.Reader;

            // Read
            _ = Task.Run(async () =>
            {
                await foreach (var item in reader.ReadAllAsync())
                {
                    await Task.Delay(1000);
                    ConsoleHelper.WriteLine($"Read: \t{item}", ConsoleColor.Green);
                }
            });

            // Write
            for (var i = 0; i < 25; i++)
            {
                await writer.WriteAsync(i);
                ConsoleHelper.WriteLine($"Write: \t{i}", ConsoleColor.Yellow);
                await Task.Delay(200);
            }

            writer.Complete();

            Console.WriteLine("Writer complete");
        }

        [DemoCaption("Channel - unbounded channel")]
        public async Task Demo3()
        {
            var queue = Channel.CreateUnbounded<int>();

            var writer = queue.Writer;
            var reader = queue.Reader;

;
            var t = Task.Run(async () =>
            {
                for (var i = 0; i < 100; i++)
                {
                    var num = i;
                    writer.TryWrite(num);
                }
            });

            await t;
            writer.Complete();

            await foreach (var n in reader.ReadAllAsync())
            {
                Console.WriteLine(n);
                await Task.Delay(100);
            }
        }
    }
}
