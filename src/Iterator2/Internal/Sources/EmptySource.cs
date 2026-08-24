using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
record EmptyIteratorSource<A>(IteratorSource? Next) : IteratorSource<A>(Next)
{
    public static readonly IteratorSource<A> Instance = new EmptyIteratorSource<A>(null!);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref OpFrame frame)
    {
        frame.SetSource(Next);
        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new SingletonSource<A>(value, this);
}
