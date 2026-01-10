using PCSP_Assignment_1.Algorithms;

namespace PCSP_Assignment_1.Core;

public class Matrix
{
    private readonly int[,] _matrix;
    private readonly int _len;
    private bool _sorted;

    public int Size => _len;
    public bool IsSorted => _sorted;

    public int this[int i, int j]
    {
        get => _matrix[i, j];
        set => _matrix[i, j] = value;
    }

    public void MarkAsSorted() => _sorted = true;

    public Matrix(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 1);
        _matrix = new int[n, n];
        _len = n;
        _sorted = false;
    }
    
    public void GenerateRandom()
        => MatrixGenerator.FillRandom(this);

    public void PlaceMaxOnDiagonal()
        => MatrixSorter.PlaceRowMaximumOnDiagonal(this);
}