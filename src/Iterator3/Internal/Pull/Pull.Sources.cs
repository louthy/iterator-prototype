using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iterator<A>(ref StackFrame frame)
    {
        unsafe
        {
            return PullGen<A>.iterator(ref frame);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        unsafe
        {
            return PullGen<A>.iterable<T, IS>()(ref frame);            
        }
    }
}
