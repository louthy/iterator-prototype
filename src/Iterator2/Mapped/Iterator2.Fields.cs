using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields2<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    public readonly K<T, A> ta;
    public readonly IteratorAction<T, IS, A, B> action;
    public readonly IS space;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal IteratorFields2(K<T, A> ta, IteratorAction<T, IS, A, B> action, in IS space)
    {
        this.ta = ta;
        this.action = action;
        this.space = space;
    }
}

/*
[SkipLocalsInit]
public ref struct IteratorFieldsMutable2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    public K<T, A> ta;
    public IteratorAction<T, IS, A>? action;
    public IS space;
}

[SkipLocalsInit]
public ref struct IteratorFieldsMutable2<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    public K<T, A> ta;
    public IteratorAction<T, IS, A, B>? action;
    public IS space;
}
*/
