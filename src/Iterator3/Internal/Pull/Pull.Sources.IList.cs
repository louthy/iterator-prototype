using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int ilistUnmanaged<A>(in StackFrame frame)
        where A : unmanaged
    {
        ref var ta = ref PullManaged.arg1<IList<A>>(in frame);
        ref var ix = ref PullUnmanaged.arg2<int>(in frame);
        if(ix >= ta.Count) return PullState.Void;
        frame.vars.PushUnmanaged(ta[ix], false);
        frame.tops.IncrementYields();
        //Log.value($"return: {x} : {Ty<A>.Pretty}", in frame);
        return PullState.Continue;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int ilistManaged<A>(in StackFrame frame)
        where A : class
    {
        ref var ta = ref PullManaged.arg1<IList<A>>(in frame);
        ref var ix = ref PullUnmanaged.arg2<int>(in frame);
        if(ix >= ta.Count) return PullState.Void;
        frame.vars.PushManaged(ta[ix], false);
        frame.tops.IncrementYields();
        //Log.value($"return: {x} : {Ty<A>.Pretty}", in frame);
        return PullState.Continue;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static int ilistStruct<A>(in StackFrame frame)
        where A : struct
    {
        ref var ta = ref PullManaged.arg1<IList<A>>(in frame);
        ref var ix = ref PullUnmanaged.arg2<int>(in frame);
        if(ix >= ta.Count) return PullState.Void;
        frame.vars.PushStruct(ta[ix], false);
        frame.tops.IncrementYields();
        //Log.value($"return: {x} : {Ty<A>.Pretty}", in frame);
        return PullState.Continue;
    }
}
