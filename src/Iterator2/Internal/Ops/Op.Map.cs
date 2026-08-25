using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal;

class MapOp<A, B>(Func<A, B> f) : Op<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame stack)
    {
        ValueStack<A>.Pop(ref stack, out var x);
        var y = f(x);
        ValueStack<B>.Push(ref stack, in y);
        return true;
    }
}
