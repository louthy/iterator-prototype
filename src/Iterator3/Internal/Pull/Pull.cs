using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int empty(in StackFrame frame) =>
        PullState.Void;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int pure(in StackFrame frame) =>
        PullState.Pure;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int pureV<A>(in StackFrame frame) =>
        arg1<A>(in frame, out var x) &&
        frame.vars.Push(in x, false)
            ? PullState.Pure
            : PullState.Void;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int incYield(in StackFrame frame)
    {
        frame.tops.IncrementYields(); 
        return PullState.Continue;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static int @continue(in StackFrame frame) =>
        PullState.Continue;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool coroutine1(in StackFrame frame) =>

        frame.StartScope();

    [MethodImpl(Optimisations.InliningOnly)]
    public static int coroutine(in StackFrame frame) =>

        coroutine1(in frame)

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int tuple<A, B>(in StackFrame frame) =>

        // Pop the second element
        pop<B>(in frame, out var b) &&

        // Pop the first element
        pop<A>(in frame, out var a) &&

        // Push the tuple
        @return(in frame, (a, b))

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int tuple<A, B, C>(in StackFrame frame) =>

        // Pop the second element
        pop<C>(in frame, out var c) &&

        // Pop the second element
        pop<B>(in frame, out var b) &&

        // Pop the first element
        pop<A>(in frame, out var a) &&

        // Push the tuple
        @return(in frame, (a, b, c)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int take(in StackFrame frame) =>

        // Pop the amount 
        arg1<int>(in frame, out var amount) && amount > 0

            // Push the updated amount
            ? update1(in frame, amount - 1) 
                  ? @continue(in frame)
                  : empty(in frame)

            // Exit!      
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int elements<A, B>(in StackFrame frame) =>

        pop<(A, B)>(in frame, out var tuple) &&
        push(in frame, in tuple.Item2)       &&
        push(in frame, in tuple.Item1)

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int elements<A, B, C>(in StackFrame frame) =>

        pop<(A, B, C)>(in frame, out var tuple) &&
        push(in frame, in tuple.Item3)          &&
        push(in frame, in tuple.Item2)          &&
        push(in frame, in tuple.Item1)

            ? @continue(in frame)
            : empty(in frame);
}