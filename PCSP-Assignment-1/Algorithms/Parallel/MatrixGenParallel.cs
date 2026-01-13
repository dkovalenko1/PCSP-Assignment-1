using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Helpers;

namespace PCSP_Assignment_1.Algorithms.Parallel;

public static class MatrixGenParallel
{
    public static void FillRandomParallel(Matrix matrix, int threadCount)
    {
        var matrixSide = matrix.SizeOfSide;
        var threads = new Thread[threadCount];
        var ranges = WorkRanges.Create(matrixSide, threadCount, true);
        var baseSeed = (ulong)Environment.TickCount64;
        for (var t = 0; t < threadCount; t++)
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
        for (var t = 0; t < threadCount; t++)
            threads[t].Join();
    }
}
