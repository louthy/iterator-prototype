#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int sextamap<A, B, C, D, E, F, G>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E, F, G>>(ref frame, out var fun) &&

        // Take the value off the stack
        pop<A, B, C, D, E, F>(ref frame, out var a, out var b, out var c, out var d, out var e, out var f) &&

        // Push the mapped value on the stack
        @return(ref frame, fun(a, b, c, d, e, f))

            ? @continue(ref frame)
            : empty(ref frame);
}