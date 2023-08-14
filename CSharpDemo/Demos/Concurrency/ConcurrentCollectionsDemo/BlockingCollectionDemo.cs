using System.Collections.Concurrent;
using Bogus;
using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Concurrency.ConcurrentCollectionsDemo
{
    public class BlockingCollectionDemo : DemoRunner<BlockingCollectionDemo>
    {
        // BlockingCollection<T> acts as a blocking queue by default
        private BlockingCollection<Person> _personsQueue = new();

        // Creating a BlockingCollection<T> with last-in, first-out (stack) semantics
        private BlockingCollection<Person> _personsStack = new(new ConcurrentStack<Person>());

        // Creating a BlockingCollection<T> with unordered (bag) semantics
        private BlockingCollection<Person> _personsBag = new(new ConcurrentBag<Person>());

        [DemoCaption("Blocking queue")]
        public async Task Demo1()
        {
            var faker = new Faker<Person>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Age, f => f.Random.Int(10, 60))
                .RuleFor(u => u.Id, f => f.IndexFaker);

            var persons = faker.Generate(5);

            // Produce
            _ = Task.Run(async () =>
            {
                foreach (var person in persons)
                {
                    _personsQueue.TryAdd(person);
                    Console.WriteLine("Write: {0}", person);
                    await Task.Delay(new Random().Next(500, 1000));
                }

                _personsQueue.CompleteAdding();
            });

            // Consume
            // loop stops when producer call CompleteAdding method, otherwise it will block
            foreach (var person in _personsQueue.GetConsumingEnumerable())
            {
                Console.WriteLine("Read: {0}", person);
            }

            // or consumer might be execute with some delay

            //while (true)
            //{
            //    _personsQueue.TryTake(out var person);
            //    Console.WriteLine("Read: {0}", person);

            //    if (_personsQueue.IsCompleted)
            //    {
            //        Console.WriteLine("Collection is completed");
            //        break;
            //    }

            //    await Task.Delay(1000);
            //}
        }

        [DemoCaption("Blocking stack")]
        public async Task Demo2()
        {
            var faker = new Faker<Person>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Age, f => f.Random.Int(10, 60))
                .RuleFor(u => u.Id, f => f.IndexFaker);

            var persons = faker.Generate(5);

            // Produce
            _ = Task.Run(async () =>
            {
                foreach (var person in persons)
                {
                    _personsStack.TryAdd(person);
                    Console.WriteLine("Write: {0}", person);
                    await Task.Delay(new Random().Next(500, 1000));
                }

                _personsStack.CompleteAdding();
            });

            // Consumer
            while (true)
            {
                _personsStack.TryTake(out var person);
                Console.WriteLine("Read: {0}", person);

                if (_personsStack.IsCompleted)
                {
                    Console.WriteLine("Collection is completed");
                    break;
                }

                await Task.Delay(1000);
            }
        }

        [DemoCaption("Blocking bag")]
        public async Task Demo3()
        {
            var faker = new Faker<Person>()
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.Age, f => f.Random.Int(10, 60))
                .RuleFor(u => u.Id, f => f.IndexFaker);

            var persons = faker.Generate(5);

            // Produce
            _ = Task.Run(async () =>
            {
                foreach (var person in persons)
                {
                    _personsBag.TryAdd(person);
                    Console.WriteLine("Write: {0}", person);
                    await Task.Delay(new Random().Next(100, 500));
                }

                _personsBag.CompleteAdding();
            });


            // Consume
            // loop stops when producer call CompleteAdding method, otherwise it will block
            foreach (var person in _personsBag.GetConsumingEnumerable())
            {
                Console.WriteLine("Read: {0}", person);
            }
        }

        public record Person
        {
            public int Id;
            public string Name;
            public int Age;

            public Person()
            {
            }
        }
    }
}
