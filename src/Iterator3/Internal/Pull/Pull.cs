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
    public static int trimap1<A, B, C, D>(ref StackFrame frame) =>

        // Peek at the map function
        pop<Func<A, B, C, D>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<(A, B, C)>(ref frame, out var abc) &&

        // Push the mapped value on the stack
        @return(ref frame, f(abc.Item1, abc.Item2, abc.Item3)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C>(ref StackFrame frame) =>

        // Pop at the apply function
        arg1<Func<A, B, C>>(ref frame,out var f) && 
        
        // Pop the second element
        pop<B>(ref frame, out var b) &&

        // Peek the first element
        peek<A>(ref frame, out var a) && 

        // Push the tuple
        @return(ref frame, f(a, b)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D>(ref StackFrame frame) =>

        // Pop at the apply function
        arg1<Func<A, B, C, D>>(ref frame,out var f) && 

        // Pop the third element
        pop<C>(ref frame, out var c) &&

        // Pop the second element
        pop<B>(ref frame, out var b) && 

        // Peek the first element
        peek<A>(ref frame, out var a) && 

        // Re-push the second element
        push(ref frame, in b) && 

        // Push the tuple
        @return(ref frame, f(a, b, c)) 

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
    public static int bind<A, B>(ref StackFrame frame) =>

        // Pop the bind function
        arg1<Func<A, Iter<B>>>(ref frame, out var f) &&

        // Take the value off the stack
        pop<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        // TODO: This should create a new source from f(a) than they yields all

        // Push the value
        @return(ref frame, f(a)) 

            ? @continue(ref frame)
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