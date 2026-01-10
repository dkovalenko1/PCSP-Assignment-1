namespace PCSP_Assignment_1.Core;

public class Matrix
{
    private readonly int[,] _matrix;
    private readonly int _len;
    private bool _sorted;

    public int Size => _len;
    public bool IsSorted => _sorted;
    public int this[int i, int j] => _matrix[i, j];

    public Matrix(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 1);
        _matrix = new int[n, n];
        _len = n;
        _sorted = false;
    }
    public void GenerateRandomMatrix()
    {
        for (var i = 0; i < _len; i++)
        {
            for (var j = 0; j < _len; j++)
                _matrix[i, j] = Random.Shared.Next();
        }
    }

    public void PlaceRowMaximumOnDiagonal()
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
        _sorted = true;
    }
}