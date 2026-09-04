using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int map<A, B>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int bimap<A, B, C>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<B>(ref frame, out var b) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a, b)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int bimap1<A, B, C>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<(A, B)>(ref frame, out var ab) &&

        // Push the mapped value on the stack
        @return(ref frame, f(ab.Item1, ab.Item2))

            ? @continue(ref frame)
            : empty(ref frame);

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
    public static int quadmap<A, B, C, D, E>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<D>(ref frame, out var d) &&

        // Take the value off the stack
        pop<C>(ref frame, out var c) &&

        // Take the value off the stack
        pop<B>(ref frame, out var b) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a, b, c, d))

            ? @continue(ref frame)
            : empty(ref frame);

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
    public static int trimap1<A, B, C, D>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<(A, B, C)>(ref frame, out var abc) &&

        // Push the mapped value on the stack
        @return(ref frame, f(abc.Item1, abc.Item2, abc.Item3)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int quadmap1<A, B, C, D, E>(ref StackFrame frame) =>

        // Peek at the map function
        arg1<Func<A, B, C, D, E>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<(A, B, C, D)>(ref frame, out var abcd) &&

        // Push the mapped value on the stack
        @return(ref frame, f(abcd.Item1, abcd.Item2, abcd.Item3, abcd.Item4)) 

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