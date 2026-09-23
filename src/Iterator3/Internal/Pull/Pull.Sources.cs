using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iterator<A>(in StackFrame frame)
    {
        unsafe
        {
            return PullGen<A>.iterator(in frame);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int iterable<T, IS, A>(in StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        unsafe
        {
            return PullGen<A>.iterable<T, IS>()(in frame);            
        }
    }
}
