using BenchmarkDotNet.Jobs;
using CSharpDemo.Helpers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CSharpDemo.Demos.Garbage_Collector
{
    public class GarbageCollectorDemo : DemoRunner<GarbageCollectorDemo>
    {
        [DemoCaption("Reclaim memory after clearing big list of int")]
        public void Demo1()
        {
            var list = getList(1_000_000_000);            

            Console.WriteLine(GC.GetTotalMemory(false)); // ~ 4 Gb

            ClearAndTrimList(list);

            Console.WriteLine(GC.GetTotalMemory(true)); // ~ 140 kB
        }

        [DemoCaption("Reclaim memory after returning from local method")]
        public void Demo2()
        {
            WorkWithList();

            Console.WriteLine(GC.GetTotalMemory(true)); // ~ 140 kB

            void WorkWithList()
            {
                var list = getList(10_000_000);
                Console.WriteLine(GC.GetTotalMemory(false));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [DemoCaption("Introduce GCHandle - application root")]
        public void Demo3()
        {
            var arr = new Byte[10];

            var handle = GCHandle.Alloc(arr, GCHandleType.Pinned);
            var addr = handle.AddrOfPinnedObject();

            unsafe
            {
                byte* ptr = (byte*)addr.ToPointer();
                for (byte i = 0; i < arr.Length; i++) 
                {
                    ptr[i] = i;
                }
            }

            handle.Free();

            ConsoleHelper.WriteCollection(arr);
            Console.WriteLine($"Address of pinned object: {(long)addr:X}");
        }

        static List<int> getList(int count)
        {
            Console.WriteLine("Populating list...");
            var list = new List<int>(count);
            foreach (var val in Enumerable.Range(0, count))
                list.Add(val);
            
            return list;
        }

        static void ClearAndTrimList(List<int> list)
        {
            list.Clear();
            list.TrimExcess();
        }
    }
}
