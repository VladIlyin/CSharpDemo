using BenchmarkDotNet.Running;

namespace CSharpDemo.Helpers;

public class DemoRunnerBenchmark<T> where T : DemoRunnerBenchmark<T>, new()
{
    public static DemoRunnerBenchmark<T> Instance => new T();

    public void RunBenchmark()
    {
        BenchmarkRunner.Run<T>();
    }
}