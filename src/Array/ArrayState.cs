namespace IteratorTest;

public readonly struct ArrayState(object items, int index, int count)
{
    public readonly object Items = items;
    public readonly int Index = index;
    public readonly int Count = count;
}
