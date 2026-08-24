using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal;

/*
class FlattenOp<A, B> : Op<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ValueStack<Iterator2<Iterator2<A>>>.Pop(ref frame, out var ta);
        
        ValueStack<B>.Push(ref frame, in y);
        return true;
    }
}*/