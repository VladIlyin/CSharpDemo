using BenchmarkDotNet.Running;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CSharpDemo.Helpers
{
    public class DemoRunnerBenchmark<T> where T : DemoRunnerBenchmark<T>, new()
    {
        public static DemoRunnerBenchmark<T> Instance => new T();

        public void RunBenchmark()
        {
            BenchmarkRunner.Run<T>();
        }
    }

    public class DemoRunner<T> where T : DemoRunner<T>, new()
    {
        public static T Instance => new T();

        public void RunBenchmark()
        {
            BenchmarkRunner.Run<T>();
        }

        public void Run(params int[] demoNumbers)
        {
            Instance.RunInternal(demoNumbers);
        }

        public async Task RunAsync(params int[] demoNumbers)
        {
            await Instance.RunInternalAsync(demoNumbers);
        }

        private void RunInternal(params int[] demoNumbers)
        {
            var demo = new T();
            if (demo is DemoRunner<T> runner)
                runner.Go(demo, demoNumbers);
            else
                throw new InvalidCastException($"T is not type of {typeof(DemoRunner<T>)}");
        }

        //public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        //{
        //    var task = (Task)@this.Invoke(obj, parameters);
        //    await task.ConfigureAwait(false);
        //    var resultProperty = task.GetType().GetProperty("Result");
        //    return resultProperty.GetValue(task);
        //}

        private void Go(T instance, params int[] demoNumbers)
        {
            var methods = instance
                .GetType()
                .GetMethods()
                .Where(x => x is { IsPublic: true, IsStatic: false } && x.Name.StartsWith("Demo"))
                .ToList();
            
            if (demoNumbers.Length == 0)
            {
                methods.ForEach(m => InvokeDemoMethod(instance, m));
            }
            else
            {
                foreach (var number in demoNumbers)
                {
                    InvokeDemoMethod(instance, methods
                                .FirstOrDefault(x => x.Name == $"Demo{number}"));
                }
            }
            
        }

        private static void InvokeDemoMethod(T instance, MethodInfo method)
        {
            if (method == null) return;

            var attr = method
                .GetCustomAttributes(typeof(DemoCaptionAttribute), true)
                .FirstOrDefault() as DemoCaptionAttribute;

            PrintCaption(attr?.Caption ?? method.Name);

            method.Invoke(instance, null);
            Console.WriteLine();
        }

        private static void PrintCaption(string caption)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{caption}\n");
            Console.ResetColor();
        }

        private async Task RunInternalAsync(params int[] demoNumbers)
        {
            var demo = new T();
            if (demo is DemoRunner<T> runner)
                await runner.GoAsync(demo, demoNumbers);
            else
                throw new InvalidCastException($"T is not type of {typeof(DemoRunner<T>)}");
        }

        private async Task GoAsync(T instance, params int[] demoNumbers)
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
                    await InvokeDemoMethodAsync(instance, method);
                }
            }
            else
            {
                foreach (var number in demoNumbers)
                {
                    await InvokeDemoMethodAsync(instance, methods
                        .FirstOrDefault(x => x.Name == $"Demo{number}"));
                }
            }

        }

        private static async Task InvokeDemoMethodAsync(T instance, MethodInfo? method)
        {
            if (method == null)
            {
                return;
            }

            var attr = method
                .GetCustomAttributes(typeof(DemoCaptionAttribute), true)
                .FirstOrDefault() as DemoCaptionAttribute;

            PrintCaption(attr?.Caption ?? method.Name);

            var task = (Task)method.Invoke(instance, null);
            await task;

            Console.WriteLine();
        }

        private static bool IsAsyncMethod(Type classType, string methodName)
        {
            // Obtain the method with the specified name.
            MethodInfo method = classType.GetMethod(methodName);

            Type attType = typeof(AsyncStateMachineAttribute);

            // Obtain the custom attribute for the method. 
            // The value returned contains the StateMachineType property. 
            // Null is returned if the attribute isn't present for the method. 
            var attrib = (AsyncStateMachineAttribute)method.GetCustomAttribute(attType);

            return (attrib != null);
        }
    }

    public class DemoCaptionAttribute : Attribute
    {
        public string Caption { get; set; }

        public DemoCaptionAttribute(string caption) => this.Caption = caption;
    }
}
