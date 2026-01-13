using System.Collections.Concurrent;
using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Helpers;

namespace PCSP_Assignment_1.Algorithms.Parallel;

public static class MatrixGenParallel
{
    public static void FillRandomParallel(Matrix matrix, int threadCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(threadCount);
        var matrixSide = matrix.SizeOfSide;
        ArgumentOutOfRangeException.ThrowIfGreaterThan(threadCount, matrixSide * matrixSide);
        var threads = new Thread[threadCount];
        var ranges = WorkRanges.CreateForMatrix(matrixSide, threadCount);
        var exceptions = new ConcurrentBag<Exception>();
        for (var t = 0; t < threadCount; t++)
        {
            var threadIndex = t;
            var range = ranges[threadIndex];
            threads[threadIndex] = new Thread(() =>
            {
                try
                {
                    var random = new Random();
                    for (var i = range.StartIndex; i < range.EndIndex; i++)
                    {
                        matrix[i] = random.Next();
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            });
            threads[threadIndex].Start();
        }
        for (var t = 0; t < threadCount; t++)
            threads[t].Join();
        if (!exceptions.IsEmpty)
            throw new AggregateException(exceptions);
    }
}
