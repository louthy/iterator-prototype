using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
abstract record IteratorSource(IteratorSource? Next, LE.Unit Dummy)
{
    [MethodImpl(Optimisations.Default)]
    public abstract bool Run(ref StackFrame stack);
    
    [MethodImpl(Optimisations.Default)]
    public IteratorSource SetParent(IteratorSource parent) =>
        this with { Next = parent };
}

[SkipLocalsInit]
abstract record IteratorSource<A>(IteratorSource? Next) : IteratorSource(Next, default)
{
    [MethodImpl(Optimisations.Default)]
    public abstract IteratorSource<A> Prepend(A value);
}
