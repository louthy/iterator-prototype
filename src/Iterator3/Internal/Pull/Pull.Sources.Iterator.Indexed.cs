using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iteratorIndexedManaged<A>(in StackFrame frame)
        where A : class
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        ref var ix = ref PullUnmanaged.arg2<int>(in frame);
        if (Iter<A>.TryRef(ref ta, out var x))
        {
            PullStruct.@return(in frame, (ix, x));
            ix++;
            return incYield(in frame);
        }
        else
        {
            return PullState.Void;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iteratorIndexedUnmanaged<A>(in StackFrame frame)
        where A : unmanaged
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        ref var ix = ref PullUnmanaged.arg2<int>(in frame);
        if (Iter<A>.TryRef(ref ta, out var x))
        {
            PullUnmanaged.@return(in frame, (ix, x));
            ix++;
            return incYield(in frame);
        }
        else
        {
            return PullState.Void;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iteratorIndexedStruct<A>(in StackFrame frame)
        where A : struct
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        ref var ix = ref PullUnmanaged.arg2<int>(in frame);
        if (Iter<A>.TryRef(ref ta, out var x))
        {
            PullStruct.@return(in frame, (ix, x));
            ix++;
            return incYield(in frame);
        }
        else
        {
            return PullState.Void;
        }
    }
}
