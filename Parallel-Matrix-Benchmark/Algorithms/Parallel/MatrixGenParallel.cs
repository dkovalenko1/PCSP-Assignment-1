using ParallelMatrixBenchmark.Core;
using ParallelMatrixBenchmark.Helpers;

namespace ParallelMatrixBenchmark.Algorithms.Parallel;

public static class MatrixGenParallel
{
    public static void FillRandomParallel(Matrix matrix, int threadCount)
    {
        var matrixSide = matrix.SizeOfSide;
        var ranges = WorkRanges.Create(matrixSide, threadCount, true);
        var threads = new Thread[ranges.Length];
        var baseSeed = (ulong)Environment.TickCount64;
        for (var t = 0; t < ranges.Length; t++)
        {
            var threadIndex = t;
            var range = ranges[threadIndex];
            threads[threadIndex] = new Thread(() =>
            {
                var threadRng = Randomizer.ForThread(threadIndex, baseSeed);
                for (var i = range.StartIndex; i < range.EndIndex; i++)
                {
                    matrix[i] = threadRng.Next();
                }
            });
            threads[threadIndex].Start();
        }
        for (var t = 0; t < threads.Length; t++)
            threads[t].Join();
    }
}
