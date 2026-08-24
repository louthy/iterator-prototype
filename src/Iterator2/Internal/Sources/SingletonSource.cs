using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
record SingletonSource<A>(A Head, IteratorSource Next) : IteratorSource<A>(Next)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref OpFrame frame)
    {
        ValueStack<A>.Push(ref frame, Head);
        frame.SetSource(Next);
        return false;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this); 
}
