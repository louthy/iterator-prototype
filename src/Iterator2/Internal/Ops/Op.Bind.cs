using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal;

class MapBind<A, B>(Func<A, Iterator2<B>> f) : Op<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ValueStack<A>.Pop(ref frame, out var a);
        var tb = f(a);
        ValueStack<Iterator2<B>>.Push(ref frame, in tb);
        return true;
    }
}
