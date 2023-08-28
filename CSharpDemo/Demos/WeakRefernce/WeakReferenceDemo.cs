using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.WeakRefernce;

public class WeakReferenceDemo : DemoRunner<WeakReferenceDemo>
{
    static WeakReference<IEnumerable<int>> _weak;

    [DemoCaption("Demo 1 - collecting weak reference after GC.Collect()")]
    public void Demo1()
    {
        RunDemoTemplateMethod.YesNoLoop(() =>
        {
            Console.WriteLine("Collect memory? (y/n)\n");
            var ch = Console.ReadKey(true).KeyChar;

            Console.WriteLine("Initializing weak reference...");

            _weak = new WeakReference<IEnumerable<int>>(GetBigList());

            Console.WriteLine($"Try to get target: {_weak.TryGetTarget(out _)}");

            Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)}");

            if (ch == 'y')
            {
                Console.WriteLine("Collecting...");
                GC.Collect();
                Console.WriteLine($"Total memory: {GC.GetTotalMemory(false)}");
            }

            Console.WriteLine($"Try to get target: {_weak.TryGetTarget(out _)}");

            static List<int> GetBigList()
            {
                return Enumerable.Range(0, 500 * 1000 * 1000).ToList();
            }
        });
    }
}