using PCSP_Assignment_1.Algorithms.Parallel;
using PCSP_Assignment_1.Algorithms.Sequential;

namespace PCSP_Assignment_1.Core;

public class Matrix
{
    private readonly int[] _matrix;
    private readonly int _n;
    private bool _sorted;

    public int SizeOfSide => _n;
    public int Size => _n * _n;
    public bool IsSorted => _sorted;

    public int this[int i, int j]
    {
        get => _matrix[i *  _n + j];
        set => _matrix[i *  _n + j] = value;
    }
    
    public int this[int n]
    {
        get => _matrix[n];
        set => _matrix[n] = value;
    }

    public void MarkAsSorted() => _sorted = true;

    public Matrix(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(n, 1);
        _matrix = new int[n*n];
        _n = n;
        _sorted = false;
    }
    
    public void GenerateRandomSeq()
        => MatrixGenSequential.FillRandomSequential(this);
    
    public void GenerateRandomParallel(int threadCount) 
        => MatrixGenParallel.FillRandomParallel(this, threadCount);

    public void PlaceMaxOnDiagonalSeq()
        => MatrixSorterSequential.PlaceRowMaximumOnDiagonalSequential(this);
}