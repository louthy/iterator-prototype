using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int trimap<A, B, C, D>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<C>(ref frame, out var c) &&

        // Take the value off the stack
        pop<B>(ref frame, out var b) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a, b, c))

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int trimap1<A, B, C, D>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<(A, B, C)>(ref frame, out var abc) &&

        // Push the mapped value on the stack
        @return(ref frame, f(abc.Item1, abc.Item2, abc.Item3)) 

            ? @continue(ref frame)
            : empty(ref frame);
}