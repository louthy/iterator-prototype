using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields2<A>
{
    public readonly object ta;
    public readonly IteratorAction<A> action;
    public readonly Space128 space;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal IteratorFields2(object ta, IteratorAction<A> action, in Space128 space)
    {
        this.ta = ta;
        this.action = action;
        this.space = space;
    }
}
