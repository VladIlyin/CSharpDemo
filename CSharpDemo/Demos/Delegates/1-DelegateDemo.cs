using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Delegates
{
    public partial class DelegateDemo : DemoRunner<DelegateDemo>
    {
        public class Person { public string Name { get; set; } }
        public class Employee : Person { }
        public class Programmer : Employee { }
        public class Identity { }
        public class Guid : Identity { }

        delegate Person GetPersonById(Guid id);

        delegate int ProcessTwoNumbers(int a, int b);

        [DemoCaption("Delegate call")]
        public void Demo1()
        {
            /*
             * with delegate keyword
             */
            ProcessTwoNumbers sum = delegate(int a, int b)
            {
                return a + b;
            };

            Console.WriteLine(sum(2, 3)); // 5

            /*
             * with lambda expression
             */
            
            ProcessTwoNumbers subtraction = (a, b) => a - b;

            // or initialization via new keyword
            // var subtraction = new ProcessTwoNumbers((a, b) => a - b);
            
            Console.WriteLine(subtraction(3, 2)); // 1
        }

        [DemoCaption("Multicast delegates")]
        public void Demo2()
        {
            ProcessTwoNumbers processTwo;

            processTwo = (a, b) => a + b;
            processTwo += (a, b) => a - b;
            processTwo += (a, b) => a * b;
            processTwo += (a, b) => a / b;

            // call all delegates, but get result of the last one
            var res = processTwo(10, 5);

            // 2
            Console.WriteLine($"Last delegate in the invocation list: {res}\n");

            // or use InvocationList
            var resList = processTwo
                .GetInvocationList()
                .Select(x => ((ProcessTwoNumbers)x).Invoke(10, 5));

            // 15 5 50 2
            ConsoleHelper.WriteCollection(resList, comment: "Results of all delegates:");
        }

        delegate void ProcessNumber(int a);

        [DemoCaption("Multicast delegates: add or remove delegates from invocation list")]
        public void Demo3()
        {
            ProcessNumber one = (a) => Console.WriteLine($"Number: {a}");
            ProcessNumber two = (a) => Console.WriteLine($"Square of {a}: {a * a}");
            ProcessNumber three = (a) => Console.WriteLine($"Sqrt of {a}: {Math.Sqrt(a)}");

            ProcessNumber processNumber;

            processNumber = one;
            processNumber += two;
            processNumber += three;

            processNumber(10);

            /*
             * Output:
             * Number: 10
             * Square of 10: 100
             * Sqrt of 10: 3,1622776601683795
             */


            processNumber -= two;
            processNumber(10);

            /*
             * Output:
             * Number: 10
             * Sqrt of 10: 3,1622776601683795
             */

            processNumber += two;
            processNumber += two;
            processNumber(10);

            /*
             * Output:
             * Sqrt of 10: 3,1622776601683795
             * Square of 10: 100
             * Square of 10: 100
             */
        }

        [DemoCaption("Combine delegates")]
        public void Demo4()
        {
            ProcessNumber one = (a) => Console.WriteLine($"Number: {a}");
            ProcessNumber two = (a) => Console.WriteLine($"Square of {a}: {a * a}");
            ProcessNumber three = (a) => Console.WriteLine($"Sqrt of {a}: {Math.Sqrt(a)}");

            var combined = Delegate.Combine(one, two, three);

            // dynamically invoke (late-bound)
            combined.DynamicInvoke(16);

            // or cast to ProcessNumber
            // ((ProcessNumber)combined)?.Invoke(16);

            /*
             * Output:
             * Number: 16
             * Square of 16: 256
             * Sqrt of 16: 4
             */

            combined = Delegate.Remove(combined, two);
            combined = Delegate.Remove(combined, three);

            // Number: 16
            ((ProcessNumber)combined)?.Invoke(16);
        }

        [DemoCaption("Delegate demo: covariance and contravariance")]
        public void Demo6()
        {
            /*
             * Delegate GetEmployeeById takes parameter
             * with type Guid and returns Person
             * Covariance and contravariance allow to assign
             * method GetPerson to the delegate
             */

            Employee GetPerson(Identity id)
            {
                return new Programmer();
            }

            GetPersonById getPerson = GetPerson;

            getPerson(new Guid());
        }
    }
}
