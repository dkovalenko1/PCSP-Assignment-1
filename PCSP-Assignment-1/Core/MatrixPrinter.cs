namespace PCSP_Assignment_1.Core;

public static class MatrixPrinter
{
    public static void Print(Matrix matrix)
    {
        Console.WriteLine("\nYour matrix:");
        for (var i = 0; i < matrix.Size; i++)
        {
            for (var j = 0; j < matrix.Size; j++)
            {
                if (i == j && matrix.IsSorted)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (i == j && !matrix.IsSorted)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                Console.Write(matrix[i, j] + " ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}