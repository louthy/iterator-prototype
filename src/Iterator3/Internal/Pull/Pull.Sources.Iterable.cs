using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int iterableUnmanaged<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : unmanaged
    {
        ref var ta = ref PullManaged.arg1<K<T, A>>(ref frame);
        ref var ts = ref PullUnmanaged.arg2<IS>(ref frame);
        if(!T.Next(in ta, ref ts, out var x)) return PullState.Void;
        frame.vars.PushUnmanaged(in x);
        return PullState.Continue;
    }
    
    [MethodImpl(Optimisations.Default)]
    public static int iterableManaged<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : class
    {
        ref var ta = ref PullManaged.arg1<K<T, A>>(ref frame);
        ref var ts = ref PullUnmanaged.arg2<IS>(ref frame);
        if(!T.Next(in ta, ref ts, out var x)) return PullState.Void;
        frame.vars.PushManaged(in x);
        return PullState.Continue;
    }
        
    [MethodImpl(Optimisations.Default)]
    public static int iterableStruct<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : struct
    {
        ref var ta = ref PullManaged.arg1<K<T, A>>(ref frame);
        ref var ts = ref PullUnmanaged.arg2<IS>(ref frame);
        if(!T.Next(in ta, ref ts, out var x)) return PullState.Void;
        frame.vars.PushStruct(in x);
        return PullState.Continue;
    }
    
    /*
        Unoptimised reference

    [MethodImpl(Optimisations.Default)]
    public static PullState iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        // Pop the iterable instance
        arg1<K<T, A>>(ref frame, out var ta) &&

        // Read the iterable state global
        arg2<IS>(ref frame, out var ts) &&

        // Step the iterable
        T.StepImmutable(in ta, in ts, out var x, out var xs) &&

        // Update the iterable state
        update1(ref frame, in xs) &&

        // Return the value
        @return(ref frame, in x)
            ? @continue(ref frame)
            : empty(ref frame);
    }
    */
}
