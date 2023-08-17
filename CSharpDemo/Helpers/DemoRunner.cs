using BenchmarkDotNet.Running;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CSharpDemo.Helpers
{
    public class DemoRunner<T> where T : DemoRunner<T>, new()
    {
        public static void Run(params int[] demoNumbers)
        {
            RunInternal(demoNumbers);
        }

        public static async Task RunAsync(params int[] demoNumbers)
        {
            await RunInternalAsync(demoNumbers);
        }

        public static void RunBenchmark()
        {
            BenchmarkRunner.Run<T>();
        }

        private static void RunInternal(params int[] demoNumbers)
        {
            var demo = new T();
            if (demo is DemoRunner<T>)
                RunDemoMethods(demo, demoNumbers);
            else
                throw new InvalidCastException($"T is not type of {typeof(DemoRunner<T>)}");
        }

        private static void RunDemoMethods(T instance, params int[] demoNumbers)
        {
            var methods = instance
                .GetType()
                .GetMethods()
                .Where(x => x is { IsPublic: true, IsStatic: false } && x.Name.StartsWith("Demo"))
                .ToList();
            
            if (demoNumbers.Length == 0)
            {
                methods.ForEach(m => RunSingleDemoMethod(instance, m));
            }
            else
            {
                foreach (var number in demoNumbers)
                {
                    RunSingleDemoMethod(
                        instance,
                        methods.FirstOrDefault(x => x.Name == $"Demo{number}"));
                }
            }
        }

        private static void RunSingleDemoMethod(T instance, MethodInfo? method)
        {
            if (method == null)
            {
                return;
            }

            var attr = method
                .GetCustomAttributes(typeof(DemoCaptionAttribute), true)
                .FirstOrDefault() as DemoCaptionAttribute;

            PrintHeaderToConsole(attr?.Caption ?? method.Name);
            method.Invoke(instance, null);
            PrintFooterToConsole();
        }

        private static void PrintHeaderToConsole(string caption)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{caption}\n");
            Console.ResetColor();
        }

        private static void PrintFooterToConsole()
        {
            Console.WriteLine();
        }

        private static async Task RunInternalAsync(params int[] demoNumbers)
        {
            var demo = new T();
            if (demo is DemoRunner<T>)
                await RunDemoMethodsAsync(demo, demoNumbers);
            else
                throw new InvalidCastException($"T is not type of {typeof(DemoRunner<T>)}");
        }

        private static async Task RunDemoMethodsAsync(T instance, params int[] demoNumbers)
        {
            var methods = instance
                .GetType()
                .GetMethods()
                .Where(x => x is { IsPublic: true, IsStatic: false } 
                            && x.Name.StartsWith("Demo") 
                            && IsAsyncMethod(instance.GetType(), x.Name))
                .ToList();

            if (demoNumbers.Length == 0)
            {
                foreach (var method in methods)
                {
                    await InvokeSingleDemoMethodAsync(instance, method);
                }
            }
            else
            {
                foreach (var number in demoNumbers)
                {
                    await InvokeSingleDemoMethodAsync(instance, methods
                        .FirstOrDefault(x => x.Name == $"Demo{number}"));
                }
            }

        }

        private static async Task InvokeSingleDemoMethodAsync(T instance, MethodInfo? method)
        {
            if (method == null)
            {
                return;
            }

            var attr = method
                .GetCustomAttributes(typeof(DemoCaptionAttribute), true)
                .FirstOrDefault() as DemoCaptionAttribute;

            PrintHeaderToConsole(attr?.Caption ?? method.Name);

            var task = (Task?)method.Invoke(instance, null);
            if (task != null) await task;

            PrintFooterToConsole();
        }

        private static bool IsAsyncMethod(Type classType, string methodName)
        {
            var method = classType.GetMethod(methodName);

            if (method is null)
            {
                return false;
            }

            var attributeType = typeof(AsyncStateMachineAttribute);
            var attribute = method.GetCustomAttribute(attributeType);

            return (attribute != null);
        }
    }
}
