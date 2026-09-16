using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int pentamap<A, B, C, D, E, F>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E, F>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<E>(ref frame, out var e) &&

        // Take the value off the stack
        pop<D>(ref frame, out var d) &&

        // Take the value off the stack
        pop<C>(ref frame, out var c) &&

        // Take the value off the stack
        pop<B>(ref frame, out var b) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a, b, c, d, e))

            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(Optimisations.Default)]
    public static int pentamap1<A, B, C, D, E, F>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E, F>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<(A, B, C, D, E)>(ref frame, out var abcde) &&

        // Push the mapped value on the stack
        @return(ref frame, f(abcde.Item1, abcde.Item2, abcde.Item3, abcde.Item4, abcde.Item5)) 

            ? @continue(ref frame)
            : empty(ref frame);
}