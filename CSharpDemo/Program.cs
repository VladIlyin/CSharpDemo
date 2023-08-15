using BenchmarkDotNet.Attributes;
using CSharpDemo;
using System.Collections.Concurrent;
using CSharpDemo.Demos.Records;
using CSharpDemo.Demos.Required_keyword;
using CSharpDemo.Demos.WeakRefernce;
using System.Text;
using System.Threading.Channels;
using CSharpDemo.Demos.AsyncAwait;
using CSharpDemo.Demos.Concurrency.ConcurrentCollectionsDemo;
using CSharpDemo.Demos.Interfaces;
using CSharpDemo.Demos.Garbage_Collector;
using CSharpDemo.Demos.Pointers;
using CSharpDemo.Demos.Collections;
using CSharpDemo.Demos.Concurrency;
using CSharpDemo.Demos.RefLocal;

public class Program
{
    public static async Task Main(string[] args)
    {
        //await BlockingCollectionDemo.Instance.RunAsync();
        //await ChannelDemo.Instance.RunAsync(3);
        //ConcurrentDictionaryDemo.Instance.Run();
        //ImmutabilityCollectionsDemo.Instance.Run();
        //DictionaryDemo.Instance.Run();
        //await CancellationDemo.Instance.RunAsync(2);
        //RecordDemo.Instance.Run();
        //await AsyncAwaitDemo.Instance.RunAsync();
        RefLocalDemo.Instance.Run();
    }
}

//DelegatesMulticastDemo.Instance.Run();
//ActionFuncDemo.Instance.Run();
//MethodOverloadingDemo.Instance.Run();
//OperatorOverloading.Instance.Run();
//ExtensionDemo.Instance.Run();
//ClosureDemo.Instance.Run();
//LinqDemo.Instance.Run();
//AsyncAwaitDemo.Instance.Run();
//AnonymousTypeDemo.Instance.Run();
//SwitchExpressionDemo.Instance.Run();

//SpanDemo.Instance.Run();
//GetHashCodeDemo.Instance.Run(3);
//IndexersDemo.Instance.Run(10);
//RecordDemo.Instance.Run();
//RequiredDemo.Instance.Run();

//WeakReferenceDemo.Instance.Run();
//GarbageCollectorDemo.Instance.Run(3);
//PointersDemo.Instance.Run();

//InterfaceDemo.Instance.Run();
//DiamondProblemDemo.Instance.Run();
//DefaultInterfaceMethodDemo.Instance.Run();

//SpanDemoBenchmark.Instance.RunBenchmark();












//BenchmarkSwitcher
//            .FromAssembly(typeof(Program).Assembly)
//            .Run(args,
//                DefaultConfig.Instance
//                    .With(new ConcurrencyVisualizerProfiler()));

//BenchmarkRunner.Run<InterlockedMultiplierBenchmark>();
//BenchmarkRunner.Run<AtomicOperationDemo>();

//AtomicOperationDemo.AtomicMultiply();
//AtomicOperationDemo.AtomicMultiply();

//Thread.Sleep(500);

//AtomicOperationDemo.AtomicMultiply();
//AtomicOperationDemo.AtomicMultiply();

//Thread.Sleep(500);

//AtomicOperationDemo.AtomicMultiply();
//AtomicOperationDemo.AtomicMultiply();

//Thread.Sleep(500);

//AtomicOperationDemo.AtomicMultiply();
//AtomicOperationDemo.AtomicMultiply();

//Thread.Sleep(500);

//AtomicOperationDemo.AtomicMultiply();
//AtomicOperationDemo.AtomicMultiply();

//Thread.Sleep(500);

//AtomicOperationDemo.AtomicMultiply();
//AtomicOperationDemo.AtomicMultiply();

//[MemoryDiagnoser]
//[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
//[RankColumn]
////[SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 5, invocationCount: 200, id: "QuickJob")]
////[ShortRunJob]
//public class InterlockedMultiplierBenchmark
//{
//    InterlockedMultiplier im;

//    [GlobalSetup]
//    public void GlobalSetup()
//    {
//        im = new InterlockedMultiplier();
//    }

//    [Benchmark]
//    public void MultiplyJustLoop()
//    {
//        double n = 1;
//        im.MultiplyJustLoop(ref n, 2);
//    }

//    [Benchmark]
//    public void MultiplyLoopSpinWait()
//    {
//        double n = 1;
//        im.MultiplyLoopSpinWait(ref n, 2);
//    }

//    [Benchmark]
//    public void MultiplyWithYield()
//    {
//        double n = 1;
//        im.MultiplyLoopThreadYield(ref n, 2);
//    }

//    [Benchmark]
//    public void MultiplyWithLock()
//    {
//        double n = 1;
//        im.MultiplyWithLock(ref n, 2);
//    }

//    [GlobalCleanup]
//    public void GlobalCleanup()
//    {
//    }
//}

//public class InterlockedMultiplier
//{
//    private readonly object lockObj = new object();

//    public int SpinCount = 0;
//    public BlockingCollection<int> SpinningThreads = new BlockingCollection<int>();

//    public void MultiplyJustLoop(ref double number, int factor)
//    {
//        while (true)
//        {
//            double snapshot1 = number;
//            double temp = snapshot1 * factor;
//            double snapshot2 = Interlocked.CompareExchange(ref number, temp, snapshot1);
//            if (snapshot1 == snapshot2)
//                return;
//        }
//    }

//    public void MultiplyLoopSpinWait(ref double number, int factor)
//    {
//        SpinWait sw = new();
//        while (true)
//        {
//            double snapshot1 = number;
//            double temp = snapshot1 * factor;
//            double snapshot2 = Interlocked.CompareExchange(ref number, temp, snapshot1);
//            if (snapshot1 == snapshot2)
//                return;

//            sw.SpinOnce();
//            //Interlocked.Increment(ref SpinCount);
//            //SpinningThreads.Add(Thread.CurrentThread.ManagedThreadId);
//        }
//    }

//    public void MultiplyLoopThreadYield(ref double number, int factor)
//    {
//        while (true)
//        {
//            double snapshot1 = number;
//            double temp = snapshot1 * factor;
//            double snapshot2 = Interlocked.CompareExchange(ref number, temp, snapshot1);
//            if (snapshot1 == snapshot2)
//                return;

//            Thread.Yield();
//        }
//    }

//    public void MultiplyWithLock(ref double number, int factor)
//    {
//        lock (lockObj)
//        {
//            number *= factor;
//        }
//    }
//}

//[MemoryDiagnoser]
//[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
//[RankColumn]
//public class AtomicOperationDemo
//{
//    //private static double n = 1;
//    private static InterlockedMultiplier multiplier = new();

//    [Benchmark]
//    public void AtomicMultiplyLoopSpinWait()
//    {
//        double n = 1;
//        List<Action> actions = new List<Action>();

//        for (int i = 0; i < 300; i++)
//        {
//            actions.Add(() => multiplier.MultiplyLoopSpinWait(ref n, 2));
//        }

//        Parallel.Invoke(actions.ToArray());
//    }

//    [Benchmark]
//    public void AtomicMultiplyWithLock()
//    {
//        double n = 1;
//        List<Action> actions = new List<Action>();

//        for (int i = 0; i < 300; i++)
//        {
//            actions.Add(() => multiplier.MultiplyWithLock(ref n, 2));
//        }

//        Parallel.Invoke(actions.ToArray());
//        //Console.WriteLine(n);
//    }
//}
