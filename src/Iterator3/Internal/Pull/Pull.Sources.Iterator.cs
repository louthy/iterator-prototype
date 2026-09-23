using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iteratorManaged<A>(in StackFrame frame)
        where A : class
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        return Iter<A>.TryRef(ref ta, out var x) &&
               PullManaged.@return(in frame, in x)
                   ? PullState.Continue
                   : PullState.Void;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iteratorUnmanaged<A>(in StackFrame frame)
        where A : unmanaged
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        return Iter<A>.TryRef(ref ta, out var x) &&
               PullUnmanaged.@return(in frame, in x)
                   ? PullState.Continue
                   : PullState.Void;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int iteratorStruct<A>(in StackFrame frame)
        where A : struct
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        return Iter<A>.TryRef(ref ta, out var x) &&
               PullStruct.@return(in frame, in x)
                   ? PullState.Continue
                   : PullState.Void;
    }
    

    /*
        Unoptimised reference

    [MethodImpl(Optimisations.Default)]
    public static int iterator<A>(in StackFrame frame) =>

        // Pop the iterator
        arg1<Iter<A>>(in frame, out var ta) &&
        
        // Read the next value
        ta.TryGetValue(out var x, out var xs) &&

        // Push the updated iterator
        update1(in frame, in xs) &&

        // Return the value
        @return(in frame, in x) 

            ? @continue(in frame)
            : empty(in frame);*/    
}
