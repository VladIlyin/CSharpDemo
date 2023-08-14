using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo.AsyncAwait
{
    public static class ReturnToCallingMethod
    {
        /*
         * Метод SaySomething помечен как асинхронный (async/await),
         * но поскольку таск возвращаемый методом не эвейтится, то на строчке метода 
         * await Task.Delay(5);
         * происходит передача управления вызывающему методу,
         * то есть выполнение метода Main продолжится после возврата из метода SayHello
         * и на консоль ничего не выведется
         */
        public static void NoAwait()
        {
            string result = "";

            SayHello();
            Console.WriteLine(result);

            async Task<string> SayHello()
            {
                await Task.Delay(5);
                result = "Hello world!";
                return "Something";
            }
        }

        /*
         * Если добавить await к вызову метода, 
         * то поток выполнения метода Main будет ожидать завершения метода
         * SayHello и на консоль выведется Hello world!
         */
        public static async Task WithAwait()
        {
            string result = "";

            await SayHello();
            Console.WriteLine(result);

            async Task<string> SayHello()
            {
                await Task.Delay(5);
                result = "Hello world!";
                return "Something";
            }
        }
    }
}
