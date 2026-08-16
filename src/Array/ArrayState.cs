using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct ArrayState(in int index, in int count)
{
    public readonly int Index = index;
    public readonly int Count = count;
}

[SkipLocalsInit]
public struct ArrayStateMutable(in int index, in int count)
{
    public int Index = index;
    public int Count = count;
}

[SkipLocalsInit]
public readonly ref struct ArrayStateRef(ref object items, ref object itemsEnd)
{
    public readonly ref object Items = ref items;
    public readonly ref object ItemsEnd = ref itemsEnd;
}

[SkipLocalsInit]
public readonly ref struct ArrayStateRef<A>(ref A items, ref A itemsEnd)
{
    public readonly ref A Items = ref items;
    public readonly ref A ItemsEnd = ref itemsEnd;
}
