namespace PCSP_Assignment_1.Core;

public class Matrix
{
    private readonly int[,] _matrix;
    private readonly int _len;

    public Matrix(int n)
    {
        _matrix = new int[n, n];
        _len = n;
        GenerateRandomMatrix();
        PrintMatrix();
        PlaceGreatestOnDiagonal();
        Console.WriteLine("---");
        PrintMatrix();
    }
    private void GenerateRandomMatrix()
    {
        for (var i = 0; i < _len; i++)
        {
            for (var j = 0; j < _len; j++)
                _matrix[i, j] = Random.Shared.Next(10, 99);
        }
    }

    private void PrintMatrix()
    {
        for (var i = 0; i < _len; i++)
        {
            for (var j =  0; j < _len; j++)
                Console.Write(_matrix[i, j] + " "); 
            Console.WriteLine();
        }
    }
    
    private void PlaceGreatestOnDiagonal()
    {
        for (var i = 0; i < _len; i++)
        {
            var greatestColIndex = 0;
            for (var j = 0; j < _len; j++)
            {
                if (_matrix[i, j] > _matrix[i, greatestColIndex])
                    greatestColIndex = j;
            }

            if (greatestColIndex != i)
                (_matrix[i, i], _matrix[i, greatestColIndex]) = (_matrix[i, greatestColIndex], _matrix[i, i]);
        }
    }
}