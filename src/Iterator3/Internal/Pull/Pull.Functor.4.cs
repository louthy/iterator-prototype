using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int quadmap<A, B, C, D, E>(in StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E>>(in frame, out var f) &&

        //Log.value("\nquadmap: pre-pop", in frame) == PullState.Continue &&

        // Take the value off the stack
        pop<A, B, C, D>(in frame, out var a, out var b, out var c, out var d) &&

        //Log.value("quadmap: post-pop", in frame) == PullState.Continue &&
        
        // Push the mapped value on the stack
        @return(in frame, f(a, b, c, d)) 

            ? @continue(in frame)
            : empty(in frame);
}