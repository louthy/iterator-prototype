using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
abstract record IteratorSource(IteratorSource? Next, LE.Unit Dummy)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public abstract bool Run(ref StackFrame frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorSource SetParent(IteratorSource parent) =>
        this with { Next = parent };
}

[SkipLocalsInit]
abstract record IteratorSource<A>(IteratorSource? Next) : IteratorSource(Next, default)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public abstract IteratorSource<A> Prepend(A value);
}
