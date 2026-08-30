using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState empty(ref StackFrame frame) =>
        PullState.Void;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState pure<A>(ref StackFrame frame) =>
        PullState.Pure;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState @continue(ref StackFrame frame) =>
        PullState.Continue;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool coroutine1(ref StackFrame frame) =>
        
        Log.coroutine("coroutine", ref frame) &&
        
        frame.StartScope();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState coroutine(ref StackFrame frame) =>

        coroutine1(ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState dup<A>(ref StackFrame frame) =>
        frame.vars.Peek<A>(out var x) &&
        frame.vars.Push(in x)         &&
        Log.function("dup", ref frame)
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState yield(ref StackFrame frame)
    {
        // Log
        frame.tops.CurrentYield++;
        Log.function("yield", ref frame);
        return @continue(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState await<A>(ref StackFrame frame) =>

        // Get the awaited value from the globals-list
        frame.globals.At<A>(frame.tops.CurrentYield, out var value) &&

        // Push the awaited value onto the 'variables' stack 
        frame.vars.Push(in value) &&

        Log.coroutine($"await {value} and push", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState await<A>(ref StackFrame frame, out A value) =>

        // Get the awaited value from the globals-list
        frame.globals.At(frame.tops.CurrentYield, out value) &&

        Log.coroutine($"await {value} and out", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState map<A, B>(ref StackFrame frame) =>

        // Peek at the map function
        constarg<Func<A, B>>(ref frame, out var f) &&

        // Take the value off the stack
        constarg<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a)) &&

        Log.function($"map swap {a} for {f(a)}", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bimap<A, B, C>(ref StackFrame frame) =>

        // Peek at the map function
        constarg<Func<A, B, C>>(ref frame, out var f) &&

        // Take the value off the stack
        constarg<B>(ref frame, out var b) &&

        // Take the value off the stack
        constarg<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a, b)) &&

        Log.function($"bimap swap [{a}, {b}] for {f(a, b)}", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bimap1<A, B, C>(ref StackFrame frame) =>

        // Peek at the map function
        constarg<Func<A, B, C>>(ref frame, out var f) &&

        // Take the value off the stack
        constarg<(A, B)>(ref frame, out var ab) &&

        // Push the mapped value on the stack
        @return(ref frame, f(ab.Item1, ab.Item2)) &&

        Log.function($"bimap swap ({ab.Item1}, {ab.Item2}) for {f(ab.Item1, ab.Item2)}", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState trimap<A, B, C, D>(ref StackFrame frame) =>

        // Peek at the map function
        constarg<Func<A, B, C, D>>(ref frame, out var f) &&

        // Take the value off the stack
        constarg<C>(ref frame, out var c) &&

        // Take the value off the stack
        constarg<B>(ref frame, out var b) &&

        // Take the value off the stack
        constarg<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        @return(ref frame, f(a, b, c)) &&

        Log.function($"bimap swap [{a}, {b}, {c}] for {f(a, b, c)}", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState trimap1<A, B, C, D>(ref StackFrame frame) =>

        // Peek at the map function
        constarg<Func<A, B, C, D>>(ref frame, out var f) &&

        // Take the value off the stack
        constarg<(A, B, C)>(ref frame, out var abc) &&

        // Push the mapped value on the stack
        @return(ref frame, f(abc.Item1, abc.Item2, abc.Item3)) &&

       Log.function($"bimap swap ({abc.Item1}, {abc.Item2}, {abc.Item3}) for {f(abc.Item1, abc.Item2, abc.Item3)}",ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState apply<A, B>(ref StackFrame frame) =>

        // Pop the second element
        constarg<B>(ref frame, out var b) &&

        // Pop the first element
        frame.vars.Peek<A>(out var a) && Log.value($"peek arg {a}", ref frame) &&

        // Push the tuple
        @return(ref frame, (a, b)) &&

        Log.function($"apply ({a}, {b})", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState tuple<A, B>(ref StackFrame frame) =>

        // Pop the second element
        constarg<B>(ref frame, out var b) &&

        // Pop the first element
        constarg<A>(ref frame, out var a) &&

        // Push the tuple
        @return(ref frame, (a, b)) &&

        Log.function($"swap [{a}, {b}] for ({a}, {b})", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState tuple<A, B, C>(ref StackFrame frame) =>

        // Pop the second element
        constarg<C>(ref frame, out var c) &&

        // Pop the second element
        constarg<B>(ref frame, out var b) &&

        // Pop the first element
        constarg<A>(ref frame, out var a) &&

        // Push the tuple
        @return(ref frame, (a, b, c)) &&

        Log.function($"swap [{a}, {b}, {c}] for ({a}, {b}, {c})", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState take(ref StackFrame frame) =>

        // Pop the amount 
        arg<int>(ref frame, out var amount, out var g) && amount > 0 &&

        Log.function($"take({amount})", ref frame) &&

        // Push the updated amount
        g.Update(ref frame, amount - 1)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bind<A, B>(ref StackFrame frame) =>

        // Pop the bind function
        constarg<Func<A, Iter<B>>>(ref frame, out var f) &&

        // Take the value off the stack
        constarg<A>(ref frame, out var a) &&

        // Push the mapped value on the stack
        // TODO: This should create a new source from f(a) than they yields all

        // Push the value
        @return(ref frame, f(a)) &&

        Log.function($"bind swap [{Log.ty<A>()} -> T<{Log.ty<B>()}>, {a}] for {f(a)}", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState elements<A, B>(ref StackFrame frame) =>

        frame.vars.Pop<(A, B)>(out var tuple) &&
        frame.vars.Push(in tuple.Item2)       &&
        frame.vars.Push(in tuple.Item1)       &&

        Log.function($"push ({tuple.Item1}, {tuple.Item2}), to [{tuple.Item1}, {tuple.Item2}]", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState elements<A, B, C>(ref StackFrame frame) =>

        frame.vars.Pop<(A, B, C)>(out var tuple) &&
        frame.vars.Push(in tuple.Item3)          &&
        frame.vars.Push(in tuple.Item2)          &&
        frame.vars.Push(in tuple.Item1)          &&

        Log.function($"push ({tuple.Item1}, {tuple.Item2}, {tuple.Item3}), to [{tuple.Item1}, {tuple.Item2}, {tuple.Item3}]", ref frame)

            ? @continue(ref frame)
            : empty(ref frame);
}