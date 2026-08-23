using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
abstract record IteratorSource(IteratorSource? Next, LE.Unit Dummy)
{
    public abstract bool Run(ref StackFrame frame);
    
    public IteratorSource SetParent(IteratorSource parent) =>
        this with { Next = parent };
}

[SkipLocalsInit]
abstract record IteratorSource<A>(IteratorSource? Next) : IteratorSource(Next, default)
{
    public abstract IteratorSource<A> Prepend(A value);
}
