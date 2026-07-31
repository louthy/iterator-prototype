namespace IteratorTest;

public readonly struct ArrayState(object items, int index, int count)
{
    public readonly object Items = items;
    public readonly int Index = index;
    public readonly int Count = count;
}

public struct ArrayStateMutable(object items, int index, int count)
{
    public object Items = items;
    public int Index = index;
    public int Count = count;
}

public readonly ref struct ArrayStateRef(ref object items, ref object itemsEnd)
{
    public readonly ref object Items = ref items;
    public readonly ref object ItemsEnd = ref itemsEnd;
}

public readonly ref struct ArrayStateRef<A>(ref A items, ref A itemsEnd)
{
    public readonly ref A Items = ref items;
    public readonly ref A ItemsEnd = ref itemsEnd;
}
