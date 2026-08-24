using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
sealed record ConsSource<A>(A Head, IteratorSource? Next) : IteratorSource<A>(Next)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref OpFrame frame)
    {
        ValueStack<A>.Push(ref frame, Head);
        frame.SetSource(Next);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this);
}
