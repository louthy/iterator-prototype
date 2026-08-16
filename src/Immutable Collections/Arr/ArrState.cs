using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct ArrState(in int index, in int count)
{
    public readonly int Index = index;
    public readonly int Count = count;
}

[SkipLocalsInit]
public struct ArrStateMutable(in int index, in int count)
{
    public int Index = index;
    public int Count = count;
}

[SkipLocalsInit]
public readonly ref struct ArrStateRef(ref object items, ref object itemsEnd)
{
    public readonly ref object Items = ref items;
    public readonly ref object ItemsEnd = ref itemsEnd;
}

[SkipLocalsInit]
public readonly ref struct ArrStateRef<A>(ref A items, ref readonly A itemsEnd)
{
    public readonly ref A Items = ref items;
    public readonly ref readonly A ItemsEnd = ref itemsEnd;
}
