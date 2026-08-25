using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype.Internal;

class FlattenOp<A> : Op<A>
{
    public static readonly Op<A> Instance = new FlattenOp<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame stack)
    {
        throw new NotImplementedException();
    }
}

record FlatSource<A>(IteratorSource? Next) : IteratorSource<A>(Next)
{
    public override bool Run(ref StackFrame stack)
    {
        throw new NotImplementedException();
    }

    public override IteratorSource<A> Prepend(A value)
    {
        throw new NotImplementedException();
    }
}