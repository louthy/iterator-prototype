using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using LanguageExt.Traits;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>

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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iterator<A>(ref StackFrame frame) =>

        // Pop the iterator
        arg<Iter<A>>(ref frame, out var ta, out var g) &&
        
        // Read the next value
        ta.TryGetValue(out var x, out var xs) &&

        // Push the updated iterator
        g.Update(ref frame, in xs) &&

        // Return the value
        @return(ref frame, in x) 

            ? @continue(ref frame)
            : empty(ref frame);
}
