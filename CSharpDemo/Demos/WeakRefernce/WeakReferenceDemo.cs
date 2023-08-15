using CSharpDemo.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Diagnostics.Tracing.Parsers.IIS_Trace;
using System.ComponentModel;
using System.Text;

namespace CSharpDemo.Demos.WeakRefernce
{
    public class WeakReferenceDemo : DemoRunner<WeakReferenceDemo>
    {
        static WeakReference<IEnumerable<int>> _weak;

        [DemoCaption("Demo 1")]
        public void Demo1()
        {
            var quit = 'y';
            while (quit == 'y')
            {
                Console.WriteLine("Collect memory? (y/n)\n");
                var ch = Console.ReadKey(true).KeyChar;

                AddWeakReference();
                TryGetWeakReferenceTarget();

                Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)}");

                if (ch == 'y')
                {
                    Collect();
                    Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)}");
                }

                TryGetWeakReferenceTarget();
                
                Console.WriteLine("\nContinue? (y/n)\n");
                quit = Console.ReadKey(true).KeyChar;
            }

            void AddWeakReference()
            {
                Console.WriteLine($"Run {nameof(AddWeakReference)} method");

                _weak = new WeakReference<IEnumerable<int>>(getList(500 * 1000 * 1000));
            }

            void TryGetWeakReferenceTarget()
            {
                Console.WriteLine($"Try to get target: {_weak.TryGetTarget(out var values)}");
            }

            void Collect()
            {
                Console.WriteLine("Collecting...");
                GC.Collect();
            }
        }

        static List<int> getList(int count)
        {
            var list = new List<int>(count);
            foreach (var val in Enumerable.Range(0, count))
                list.Add(val);

            return list;
        }
    }
}
