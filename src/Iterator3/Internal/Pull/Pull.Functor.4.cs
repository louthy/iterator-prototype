using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int quadmap<A, B, C, D, E>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E>>(ref frame, out var f) &&

        Log.value("\nquadmap: pre-pop", ref frame) == PullState.Continue &&

        // Take the value off the stack
        pop<A, B, C, D>(ref frame, out var a, out var b, out var c, out var d) &&

        Log.value("quadmap: post-pop", ref frame) == PullState.Continue &&
        
        // Push the mapped value on the stack
        @return(ref frame, f(a, b, c, d)) 

            ? @continue(ref frame)
            : empty(ref frame);
}