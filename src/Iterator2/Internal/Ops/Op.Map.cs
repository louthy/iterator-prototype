using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal;

class MapOp<A, B>(Func<A, B> f) : Op<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref OpFrame frame)
    {
        ValueStack<A>.Pop(ref frame, out var x);
        var y = f(x);
        ValueStack<B>.Push(ref frame, in y);
        return true;
    }
}
