using BenchmarkDotNet.Attributes;
using CSharpDemo.Helpers;

namespace CSharpDemo
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [ShortRunJob]
    public class SpanDemoBenchmark : DemoRunnerBenchmark<SpanDemoBenchmark>
    {
        [Params("03 06 2019")]
        public string Dte { get; set; }

        [GlobalSetup]
        public void Setup()
        {

        }

        [Benchmark(Baseline = true)]
        public DateTime StringToDateTimeOrdinary()
        {
            var day = Dte.Substring(0, 2);
            var month = Dte.Substring(3, 2);
            var year = Dte.Substring(6);

            return new DateTime(
                int.Parse(year),
                int.Parse(month),
                int.Parse(day));
        }

        [Benchmark]
        public DateTime StringToDateTimeWithParse()
        {
            return DateTime.ParseExact(Dte, "dd MM yyyy", null);
        }

        [Benchmark]
        public DateTime StringToDateTimeWithSpan()
        {
            Span<char> span = Dte.ToCharArray();

            var spanDay = span[..2];
            var spanMonth = span[3..5];
            var spanYear = span[6..];

            return new DateTime(
                int.Parse(spanYear),
                int.Parse(spanMonth),
                int.Parse(spanDay));
        }
    }
}
