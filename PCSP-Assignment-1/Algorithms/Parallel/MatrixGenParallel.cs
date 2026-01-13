using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Helpers;

namespace PCSP_Assignment_1.Algorithms.Parallel;

public static class MatrixGenParallel
{
    public static void FillRandomParallel(Matrix matrix, int threadCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(threadCount);
        var matrixSide = matrix.SizeOfSide;
        var threads = new Thread[threadCount];
        var ranges = WorkRanges.CreateForMatrix(matrixSide, threadCount);
        for (var t = 0; t < threadCount; t++)
        {
            var threadIndex = t;
            var range = ranges[threadIndex];
            threads[threadIndex] = new Thread(() =>
            {
                try
                {
                    for (var i = range.StartIndex; i < range.EndIndex; i++)
                    {
                        matrix[i] = Random.Shared.Next();
                    }
                    Console.WriteLine($"Thread {threadIndex + 1} finished");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            threads[threadIndex].Start();
        }
        for (var t = 0; t < threadCount; t++)
            threads[t].Join();
        Console.WriteLine("All threads finished");
    }
}
