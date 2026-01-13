namespace PCSP_Assignment_1.Helpers;

public struct WorkRange(int startIndex, int endIndex)
{
    public readonly int StartIndex = startIndex;
    public readonly int EndIndex = endIndex;
}

public static class WorkRanges
{
    public static WorkRange[] Create(int matrixSide, int threadCount, bool forGen)
    {
        if (!forGen)
            ArgumentOutOfRangeException.ThrowIfGreaterThan(threadCount, matrixSide);
        var ranges = new WorkRange[threadCount];
        var matrixSize = matrixSide * matrixSide;
        int baseSize = forGen ? matrixSize / threadCount : matrixSide / threadCount;
        var rem = forGen ? matrixSize % threadCount : matrixSide % threadCount;
        var start = 0;
        for (var t = 0; t < threadCount; t++)
        {
            var size = baseSize + (t < rem ? 1 : 0);
            var end = start + size;
            ranges[t] = new WorkRange(start, end);
            start = end;
        }
        return ranges;
    }
}