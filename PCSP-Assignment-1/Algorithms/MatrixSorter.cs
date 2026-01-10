using PCSP_Assignment_1.Core;

namespace PCSP_Assignment_1.Algorithms;

public static class MatrixSorter
{
    public static void PlaceRowMaximumOnDiagonal(Matrix matrix)
    {
        for (var i = 0; i < matrix.Size; i++)
        {
            var maxColIndex = 0;
            for (var j = 0; j < matrix.Size; j++)
            {
                if (matrix[i, j] > matrix[i, maxColIndex])
                    maxColIndex = j;
            }

            if (maxColIndex != i)
                (matrix[i, i], matrix[i, maxColIndex]) = (matrix[i, maxColIndex], matrix[i, i]);
        }

        matrix.MarkAsSorted();
    }
}