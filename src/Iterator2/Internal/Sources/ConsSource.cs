using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
sealed record ConsSource<A>(A Head, IteratorSource? Next) : IteratorSource<A>(Next)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override unsafe bool Run(ref StackFrame frame)
    {
        ValueStack<A>.Push(ref frame, Head);
        frame.Source = Next;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this);
}
