using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields
{
    public readonly object ta;
    public readonly IteratorAction action;
    public readonly Space128 space;

    [MethodImpl(Optimisations.Default)]
    internal IteratorFields(object ta, IteratorAction action, in Space128 space)
    {
        this.ta = ta;
        this.action = action;
        this.space = space;
    }
}

[SkipLocalsInit]
public readonly struct IteratorFields<A>
{
    public readonly object ta;
    public readonly IteratorAction<A> action;
    public readonly Space128 space;

    [MethodImpl(Optimisations.Default)]
    internal IteratorFields(object ta, IteratorAction<A> action, in Space128 space)
    {
        this.ta = ta;
        this.action = action;
        this.space = space;
    }
}
