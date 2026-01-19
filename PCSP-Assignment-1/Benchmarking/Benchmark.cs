using System.Globalization;
using PCSP_Assignment_1.Core;

namespace PCSP_Assignment_1.Benchmarking;

public class Benchmark
{
    private readonly int[] _sizes = [50, 100, 500, 1000, 10000, 20000, 30000];
    private readonly int[] _threads;
    private readonly List<string> _csvLines = [];

    public Benchmark()
    {
        var cores = Environment.ProcessorCount;
        _threads = [cores / 6, cores / 4, cores / 2, 8, 10, cores, cores * 2,  cores * 4,   cores * 8, cores * 16];
    }

    public void Run()
    {
        Console.WriteLine($"\nCore count: {Environment.ProcessorCount}");
        Console.WriteLine($"Thread configs: {string.Join(", ", _threads)}\n");

        _csvLines.Add("Size,Threads,GenTime_ms,SortTime_ms,TotalTime_ms");

        foreach (var size in _sizes)
        {
            var (genTime, sortTime) = MeasureSequential(size);
            _csvLines.Add($"{size},1,{genTime:F3},{sortTime:F3},{genTime + sortTime:F3}");
        }

        foreach (var size in _sizes)
        {
            foreach (var threads in _threads)
            {
                var (genTime, sortTime) = MeasureParallel(size, threads);
                _csvLines.Add($"{size},{threads},{genTime:F3},{sortTime:F3},{genTime + sortTime:F3}");
            }
        }

        var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "benchmark_results.csv");
        File.WriteAllLines(csvPath, _csvLines);
        Console.WriteLine($"\nResults saved to {csvPath}");
    }

    private static (double genTime, double sortTime) MeasureSequential(int size)
    {
        var warmup = new Matrix(100);
        warmup.GenerateRandomSeq();
        warmup.PlaceMaxOnDiagonalSeq();

        var genTimes = new List<double>();
        var sortTimes = new List<double>();

        for (var i = 0; i < 5; i++)
        {
            var m = new Matrix(size);
            genTimes.Add(ActionTimer.Measure(m.GenerateRandomSeq).TotalMilliseconds);
            sortTimes.Add(ActionTimer.Measure(m.PlaceMaxOnDiagonalSeq).TotalMilliseconds);
        }

        return (genTimes.Average(), sortTimes.Average());
    }

    private static (double genTime, double sortTime) MeasureParallel(int size, int threads)
    {
        var warmup = new Matrix(100);
        warmup.GenerateRandomParallel(threads);
        warmup.PlaceMaxOnDiagonalParallel(threads);

        var genTimes = new List<double>();
        var sortTimes = new List<double>();

        for (var i = 0; i < 5; i++)
        {
            var m = new Matrix(size);
            genTimes.Add(ActionTimer.Measure(() => m.GenerateRandomParallel(threads)).TotalMilliseconds);
            sortTimes.Add(ActionTimer.Measure(() => m.PlaceMaxOnDiagonalParallel(threads)).TotalMilliseconds);
        }

        return (genTimes.Average(), sortTimes.Average());
    }
}