using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Helpers;

namespace PCSP_Assignment_1.Algorithms.Parallel;

public static class MatrixSorterParallel
{
    public static void PlaceRowMaximumOnDiagonalParallel(Matrix matrix, int threadCount)
    {
        var matrixSide = matrix.SizeOfSide;
        var threads = new Thread[threadCount];
        var ranges = WorkRanges.Create(matrixSide, threadCount, false);
        for (var t = 0; t < threadCount; t++)
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
        for (var t = 0; t < threadCount; t++)
            threads[t].Join();
        matrix.MarkAsSorted();
    }
}