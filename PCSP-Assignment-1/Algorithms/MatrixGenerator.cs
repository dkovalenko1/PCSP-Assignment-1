using PCSP_Assignment_1.Core;

namespace PCSP_Assignment_1.Algorithms;

public static class MatrixGenerator
{
    public static void FillRandom(Matrix matrix)
    {
        for (var i = 0; i < matrix.Size; i++)
        {
            for (var j = 0; j < matrix.Size; j++)
                matrix[i, j] = Random.Shared.Next();
        }
    }
}