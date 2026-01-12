using PCSP_Assignment_1.Core;

namespace PCSP_Assignment_1.Algorithms.Sequential;

public static class MatrixGenSequential
{
    public static void FillRandomSequential(Matrix matrix)
    {
        for (var i = 0; i < matrix.SizeOfSide; i++)
        {
            for (var j = 0; j < matrix.SizeOfSide; j++)
                matrix[i, j] = Random.Shared.Next();
        }
    }
}