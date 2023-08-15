using CSharpDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.Demos.AsyncAwait
{
    public class AsyncAwaitDemo : DemoRunner<AsyncAwaitDemo>
    {
        private static string greeting;

        [DemoCaption("Async/await: async method call is not awaited")]
        public async Task Demo1()
        {
            /*
             * Метод SaySomething помечен как асинхронный (async/await),
             * но поскольку таск возвращаемый методом не эвейтится, то на строчке метода 
             * await Task.Delay(5); происходит передача управления вызывающему методу,
             * то есть выполнение метода Main продолжится после возврата из метода SayHello
             * и на консоль ничего не выведется
             */
            SayHi("Jane");
            Console.WriteLine(greeting);

            /*
             * Если добавить await к вызову метода, 
             * то поток выполнения метода Main будет ожидать завершения метода
             * SayHello и на консоль выведется Hello world!
             */
            await SayHi("Alice");
            Console.WriteLine(greeting);

            async Task SayHi(string name)
            {
                greeting = "No greeting";
                await Task.Delay(5);
                greeting = $"Hi, {name}!";
            }
        }
    }
}
