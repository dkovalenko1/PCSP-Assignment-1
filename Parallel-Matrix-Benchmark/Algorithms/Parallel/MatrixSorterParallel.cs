using ParallelMatrixBenchmark.Core;
using ParallelMatrixBenchmark.Helpers;

namespace ParallelMatrixBenchmark.Algorithms.Parallel;

public static class MatrixSorterParallel
{
    public static void PlaceRowMaximumOnDiagonalParallel(Matrix matrix, int threadCount)
    {
        var matrixSide = matrix.SizeOfSide;
        var ranges = WorkRanges.Create(matrixSide, threadCount, false);
        var threads = new Thread[ranges.Length];
        for (var t = 0; t < ranges.Length; t++)
        {
            var threadIndex = t;
            var range = ranges[threadIndex];
            threads[threadIndex] = new Thread(() =>
            {
                
                for (var i = range.StartIndex; i < range.EndIndex; i++)
                {
                    var maxColIndex = 0;
                    for (var j = 0; j < matrix.SizeOfSide; j++)
                    {
                        if (matrix[i, j] > matrix[i, maxColIndex])
                            maxColIndex = j;
                    }

                    if (maxColIndex != i)
                        (matrix[i, i], matrix[i, maxColIndex]) = (matrix[i, maxColIndex], matrix[i, i]);
                }
            });
            threads[threadIndex].Start();
        }
        for (var t = 0; t < threads.Length; t++)
            threads[t].Join();
        matrix.MarkAsSorted();
    }
}