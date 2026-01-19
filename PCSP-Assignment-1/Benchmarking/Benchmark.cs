using System.Globalization;
using PCSP_Assignment_1.Core;

namespace PCSP_Assignment_1.Benchmarking;

public class Benchmark
{
    private readonly int[] _sizes = [50, 100, 500, 1000, 5000, 10000, 15000, 20000, 25000, 30000];
    private readonly int[] _threads;
    private readonly List<string> _csvLines = [];

    public Benchmark()
    {
        var cores = Environment.ProcessorCount;
        _threads = [cores / 6, cores / 4, cores / 2, 8, 10, cores, 16, 20, cores * 2,  cores * 4,   cores * 8, cores * 16, cores * 20];
    }

    public void Run()
    {
        Console.WriteLine($"\nCore count: {Environment.ProcessorCount}");
        Console.WriteLine($"Thread configs: {string.Join(", ", _threads)}\n");

        _csvLines.Add("Size,Threads,GenTime_ms,SortTime_ms,TotalTime_ms");

        foreach (var size in _sizes)
        {
            var (genTime, sortTime) = MeasureSequential(size);
            _csvLines.Add(string.Format(CultureInfo.InvariantCulture,
                "{0},1,{1:F3},{2:F3},{3:F3}", size, genTime, sortTime, genTime + sortTime));
        }

        foreach (var size in _sizes)
        {
            foreach (var threads in _threads)
            {
                var (genTime, sortTime) = MeasureParallel(size, threads);
                _csvLines.Add(string.Format(CultureInfo.InvariantCulture,
                    "{0},{1},{2:F3},{3:F3},{4:F3}", size, threads, genTime, sortTime, genTime + sortTime));
            }
        }

        var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "benchmark_results.csv");
        File.WriteAllLines(csvPath, _csvLines);
        Console.WriteLine($"\nResults saved to {csvPath}");
    }

    private static (double genTime, double sortTime) MeasureSequential(int size)
    {
        for (var w = 0; w < 3; w++)
        {
            var warmup = new Matrix(size);
            warmup.GenerateRandomSeq();
            warmup.PlaceMaxOnDiagonalSeq();
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var genTimes = new List<double>();
        var sortTimes = new List<double>();

        for (var i = 0; i < 5; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var m = new Matrix(size);
            genTimes.Add(ActionTimer.Measure(m.GenerateRandomSeq).TotalMilliseconds);
            sortTimes.Add(ActionTimer.Measure(m.PlaceMaxOnDiagonalSeq).TotalMilliseconds);
        }

        return (Median(genTimes), Median(sortTimes));
    }

    private static (double genTime, double sortTime) MeasureParallel(int size, int threads)
    {
        for (var w = 0; w < 3; w++)
        {
            var warmup = new Matrix(size);
            warmup.GenerateRandomParallel(threads);
            warmup.PlaceMaxOnDiagonalParallel(threads);
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var genTimes = new List<double>();
        var sortTimes = new List<double>();

        for (var i = 0; i < 5; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var m = new Matrix(size);
            genTimes.Add(ActionTimer.Measure(() => m.GenerateRandomParallel(threads)).TotalMilliseconds);
            sortTimes.Add(ActionTimer.Measure(() => m.PlaceMaxOnDiagonalParallel(threads)).TotalMilliseconds);
        }

        return (Median(genTimes), Median(sortTimes));
    }

    private static double Median(List<double> values)
    {
        var sorted = values.OrderBy(x => x).ToList();
        var count = sorted.Count;
        if (count % 2 == 0)
            return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
        return sorted[count / 2];
    }
}