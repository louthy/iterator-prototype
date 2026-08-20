using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    public readonly K<T, A> ta;
    public readonly IteratorAction<A>? action;
    public readonly IS space;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal IteratorFields2(K<T, A> ta, in IS space)
    {
        this.ta = ta;
        this.space = space;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal IteratorFields2(K<T, A> ta, IteratorAction<A> action, in IS space)
    {
        this.ta = ta;
        this.action = action;
        this.space = space;
    }
}
