using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int empty(ref StackFrame frame) =>
        PullState.Void;

    [MethodImpl(Optimisations.Default)]
    public static int pure(ref StackFrame frame) =>
        PullState.Pure;

    [MethodImpl(Optimisations.Default)]
    public static int pureV<A>(ref StackFrame frame) =>
        arg1<A>(ref frame, out var x) &&
        frame.vars.Push(in x)
            ? PullState.Pure
            : PullState.Void;

    [MethodImpl(Optimisations.Default)]
    public static int @continue(ref StackFrame frame) =>
        PullState.Continue;

    [MethodImpl(Optimisations.Default)]
    public static bool coroutine1(ref StackFrame frame) =>

        frame.StartScope();

    [MethodImpl(Optimisations.Default)]
    public static int coroutine(ref StackFrame frame) =>

        coroutine1(ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int dup<A>(ref StackFrame frame) =>
        frame.vars.Peek<A>(out var x) &&
        frame.vars.Push(in x)         
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int tuple<A, B>(ref StackFrame frame) =>

        // Pop the second element
        pop<B>(ref frame, out var b) &&

        // Pop the first element
        pop<A>(ref frame, out var a) &&

        // Push the tuple
        @return(ref frame, (a, b))

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int tuple<A, B, C>(ref StackFrame frame) =>

        // Pop the second element
        pop<C>(ref frame, out var c) &&

        // Pop the second element
        pop<B>(ref frame, out var b) &&

        // Pop the first element
        pop<A>(ref frame, out var a) &&

        // Push the tuple
        @return(ref frame, (a, b, c)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int take(ref StackFrame frame) =>

        // Pop the amount 
        arg1<int>(ref frame, out var amount) && amount > 0

            // Push the updated amount
            ? update1(ref frame, amount - 1) 
                  ? @continue(ref frame)
                  : empty(ref frame)

            // Exit!      
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int elements<A, B>(ref StackFrame frame) =>

        pop<(A, B)>(ref frame, out var tuple) &&
        push(ref frame, in tuple.Item2)       &&
        push(ref frame, in tuple.Item1)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int elements<A, B, C>(ref StackFrame frame) =>

        pop<(A, B, C)>(ref frame, out var tuple) &&
        push(ref frame, in tuple.Item3)          &&
        push(ref frame, in tuple.Item2)          &&
        push(ref frame, in tuple.Item1)

            ? @continue(ref frame)
            : empty(ref frame);
}