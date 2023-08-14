using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Delegates
{
    public class DelegatesMulticastDemo : DemoRunner<DelegatesMulticastDemo>
    {
        delegate int ProcessTwoNumbers(int a, int b);
        delegate void ProcessNumber(int a);

        [DemoCaption("Multicast delegates")]
        public void Demo1()
        {
            ProcessTwoNumbers processTwo;

            processTwo = (a, b) => a + b;
            processTwo += (a, b) => a - b;
            processTwo += (a, b) => a * b;
            processTwo += (a, b) => a / b;

            //call all delegates, but get result of the last one
            var res = processTwo(10, 5); // 2

            Console.WriteLine($"Last delegate in the invocation list: {res}\n");

            // or use InvocationList
            var resList = processTwo
                .GetInvocationList()
                .Select(x => ((ProcessTwoNumbers)x).Invoke(10, 5));

            ConsoleHelper.WriteLineCollection(resList, "Results of all delegates:");
        }

        [DemoCaption("Multicast delegates: add or remove delegates from invocation list")]
        public void Demo2()
        {
            ProcessNumber processNumber;

            ProcessNumber one = (a) => Console.WriteLine($"Number: {a}");
            ProcessNumber two = (a) => Console.WriteLine($"Square of {a}: {a * a}");
            ProcessNumber three = (a) => Console.WriteLine($"Sqrt of {a}: {Math.Sqrt(a)}");

            processNumber = one;
            processNumber += two;
            processNumber += three;
            processNumber(10); // 10, 100, 3,1622776601683795


            processNumber -= three;
            processNumber(10); // 10, 100


            processNumber += two;
            processNumber += two;
            processNumber(10); // 10, 100, 100, 100
        }

        [DemoCaption("Combine delegates")]
        public void Demo3()
        {
            ProcessNumber one = (a) => Console.WriteLine($"Number: {a}");
            ProcessNumber two = (a) => Console.WriteLine($"Square of {a}: {a * a}");
            ProcessNumber three = (a) => Console.WriteLine($"Sqrt of {a}: {Math.Sqrt(a)}");

            var combined = Delegate.Combine(one, two, three);
            ((ProcessNumber)combined)?.Invoke(16);

            combined = Delegate.Remove(combined, two);
            combined = Delegate.Remove(combined, three);
            ((ProcessNumber)combined)?.Invoke(16);
        }

        delegate int GetValue();

        [DemoCaption("Multicast delegate: returns result from the last delegate in the chain")]
        public void Demo4()
        {
            int val1() { return 1; }
            int val2() { return 2; }

            var v1 = new GetValue(val1);
            var v2 = new GetValue(val2);
            
            var chain = v1;
            chain += v2;

            Console.WriteLine(chain());
        }
    }
}
