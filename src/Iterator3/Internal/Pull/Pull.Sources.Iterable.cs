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
        PullManaged.arg2<K<T, A>>(ref frame, out var ta);
        ref var ts = ref PullUnmanaged.arg1<IS>(ref frame);
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
        PullManaged.arg2<K<T, A>>(ref frame, out var ta);
        ref var ts = ref PullUnmanaged.arg1<IS>(ref frame);
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
        PullManaged.arg2<K<T, A>>(ref frame, out var ta);
        ref var ts = ref PullUnmanaged.arg1<IS>(ref frame);
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
        constarg<K<T, A>>(ref frame, out var ta) &&

        // Read the iterable state global
        arg<IS>(ref frame, out var ts, out var gts) &&

        // Step the iterable
        T.StepImmutable(in ta, in ts, out var x, out var xs) &&

        // Update the iterable state
        gts.Update(ref frame, in xs) &&

        // Return the value
        @return(ref frame, in x)
            ? @continue(ref frame)
            : empty(ref frame);
    }
    */
}
