using System.Diagnostics;

namespace ParallelMatrixBenchmark.Benchmarking;

public static class ActionTimer
{
    public static TimeSpan Measure(Action action)
    {
        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

}