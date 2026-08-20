using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields2<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    public readonly K<T, A> ta;
    public readonly IteratorAction<B> action;
    public readonly IS space;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal IteratorFields2(K<T, A> ta, IteratorAction<B> action, in IS space)
    {
        this.ta = ta;
        this.action = action;
        this.space = space;
    }
}
