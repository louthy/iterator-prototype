using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int pentamap<A, B, C, D, E, F>(in StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E, F>>(in frame, out var f) &&

        // Take the value off the stack
        pop<A, B, C, D, E>(in frame, out var a, out var b, out var c, out var d, out var e) &&

        // Push the mapped value on the stack
        @return(in frame, f(a, b, c, d, e)) 

            ? @continue(in frame)
            : empty(in frame);
}