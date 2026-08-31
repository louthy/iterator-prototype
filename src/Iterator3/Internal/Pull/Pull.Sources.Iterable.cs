using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static PullState iterableUnmanaged<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : unmanaged
    {
        frame.vars.PopManaged<K<T, A>>(out var ta);
        frame.vars.PopUnmanaged<Global<IS>>(out var gts);
        ref var ts = ref frame.globals.AtUnmanaged<IS>(gts.Index);
        if(!T.Next(in ta, ref ts, out var x)) return empty(ref frame);
        frame.vars.PushUnmanaged(in x);
        return PullState.Continue;
    }
    
    [MethodImpl(Optimisations.Default)]
    public static PullState iterableManaged<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : class
    {
        frame.vars.PopManaged<K<T, A>>(out var ta);
        frame.vars.PopUnmanaged<Global<IS>>(out var gts);
        ref var ts = ref frame.globals.AtUnmanaged<IS>(gts.Index);
        if(!T.Next(in ta, ref ts, out var x)) return empty(ref frame);
        frame.vars.PushManaged(in x);
        return PullState.Continue;
    }
        
    [MethodImpl(Optimisations.Default)]
    public static PullState iterableStruct<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
        where A : struct
    {
        frame.vars.PopManaged<K<T, A>>(out var ta);
        frame.vars.PopUnmanaged<Global<IS>>(out var gts);
        ref var ts = ref frame.globals.AtUnmanaged<IS>(gts.Index);
        if(!T.Next(in ta, ref ts, out var x)) return empty(ref frame);
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
