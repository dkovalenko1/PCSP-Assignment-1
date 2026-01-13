namespace PCSP_Assignment_1.Helpers;

public struct Randomizer
{
    private uint _state;

    private const uint GoldenRatio = 0x9e3779b9;

    public Randomizer(uint seed)
    {
        _state = seed == 0 ? 123456789u : seed;
    }
    
    public int Next()
    {
        _state ^= _state << 13;
        _state ^= _state >> 17;
        _state ^= _state << 5;

        return (int)(_state & 0x7FFFFFFF);
    }

    public static uint CreateThreadSeed(int threadIndex, ulong? baseSeed = null)
    {
        var seed = baseSeed ?? (ulong)Environment.TickCount64;
        return (uint)(seed + (uint)threadIndex * GoldenRatio);
    }
    public static Randomizer ForThread(int threadIndex, ulong? baseSeed = null)
    {
        return new Randomizer(CreateThreadSeed(threadIndex, baseSeed));
    }
}