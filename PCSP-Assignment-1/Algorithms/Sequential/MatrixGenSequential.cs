using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Helpers;

namespace PCSP_Assignment_1.Algorithms.Sequential;

public static class MatrixGenSequential
{
    public static void FillRandomSequential(Matrix matrix)
    {
        var baseSeed = (ulong)Environment.TickCount64;
        var rng = new Randomizer(123676767);
        for (var i = 0; i < matrix.SizeOfSide; i++)
        {
            for (var j = 0; j < matrix.SizeOfSide; j++)
                matrix[i, j] = rng.Next();
        }
    }
}