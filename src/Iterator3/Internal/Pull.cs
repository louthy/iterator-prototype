using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static class Pull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState empty(ref StackFrame frame)
    {
        frame.VoidCoRoutine();
        return PullState.Void;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState @continue(ref StackFrame frame) =>
        PullState.Continue;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState coroutine(ref StackFrame frame) =>
        frame.StartNoArgCoRoutine() 
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState coroutine<A>(ref StackFrame frame) =>
        frame.StartCoRoutine<A>() 
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool globalM<A>(ref StackFrame frame, out Global<A> global) =>

        // Load global 
        frame.vars.Pop(out global);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool global<A>(ref StackFrame frame) =>
        frame.vars.Pop<Global<A>>(out var g) &&
        frame.vars.Push(in g.Value(ref frame));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState dup<A>(ref StackFrame frame) =>
        
        // Peek the top value
        frame.vars.Peek<A>(out var value) &&
        
        // Duplicate it on the stack
        frame.vars.Push(in value) 

            ? @continue(ref frame)
            : empty(ref frame);
            

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState yield<A>(ref StackFrame frame) =>
        
        // Create a new 'yield' co-routine scope with a unit input argument.
        // The top of the stack will become a yielded value
        frame.StartYield<A>()

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState yield<A>(ref StackFrame frame, in A value) =>
        
        // Create a new 'yield' co-routine scope with a unit input argument
        frame.StartYield(in value)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState await<A>(ref StackFrame frame) =>

        // Await a value
        frame.yields.Pop<A>(out var value) &&

        // Push the awaited value onto the 'variables' stack 
        frame.vars.Push(in value)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState await<A>(ref StackFrame frame, out A value) =>
        
        // NOTE: No input value is touched; and therefore, it becomes the output value also.
        //       The `A` value only leaves the 'await queue' and becomes the `out` value
        
        // Await the value
        frame.yields.Pop(out value)  

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState pure<A>(ref StackFrame frame) =>
        
        // Pop the program-counter, so we can exit this subroutine 
        frame.EndCoRoutine<A>()
        
            ? PullState.Pure
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState map<A, B>(ref StackFrame frame) =>
        
        // Peek at the map function
        frame.vars.Pop<Func<A, B>>(out var f) &&

        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(a))
        
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bimap<A, B, C>(ref StackFrame frame) =>
        
        // Peek at the map function
        frame.vars.Pop<Func<A, B, C>>(out var f) &&

        // Take the value off the stack
        frame.vars.Pop<B>(out var b) &&

        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(a, b))
        
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bimap1<A, B, C>(ref StackFrame frame) =>
        
        // Peek at the map function
        frame.vars.Pop<Func<A, B, C>>(out var f) &&

        // Take the value off the stack
        frame.vars.Pop<(A, B)>(out var ab) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(ab.Item1, ab.Item2))
        
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState trimap<A, B, C, D>(ref StackFrame frame) =>
        
        // Peek at the map function
        frame.vars.Pop<Func<A, B, C, D>>(out var f) &&

        // Take the value off the stack
        frame.vars.Pop<C>(out var c) &&

        // Take the value off the stack
        frame.vars.Pop<B>(out var b) &&

        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(a, b, c))
        
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState trimap1<A, B, C, D>(ref StackFrame frame) =>
        
        // Peek at the map function
        frame.vars.Pop<Func<A, B, C, D>>(out var f) &&

        // Take the value off the stack
        frame.vars.Pop<(A, B, C)>(out var abc) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(abc.Item1, abc.Item2, abc.Item3))
        
            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState tuple<A, B>(ref StackFrame frame) =>
        
        // Pop the second element
        frame.vars.Pop<B>(out var b) &&
        
        // Pop the first element
        frame.vars.Pop<A>(out var a) &&
        
        // Push the tuple
        frame.vars.Push((a, b)) 
        
            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState tuple<A, B, C>(ref StackFrame frame) =>
        
        // Pop the second element
        frame.vars.Pop<C>(out var c) &&
        
        // Pop the second element
        frame.vars.Pop<B>(out var b) &&
        
        // Pop the first element
        frame.vars.Pop<A>(out var a) &&
        
        // Push the tuple
        frame.vars.Push((a, b, c)) 
        
            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState tuple1<A, B, C>(ref StackFrame frame) =>
        
        // Pop the second element
        frame.vars.Pop<C>(out var c) &&
        
        // Pop the second element
        frame.vars.Pop<(A, B)>(out var ab) &&
        
        // Push the tuple
        frame.vars.Push((ab.Item1, ab.Item2, c)) 
        
            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState take(ref StackFrame frame) =>
        
        // Peek at the amount
        globalM<int>(ref frame, out var amount) && amount.Value(ref frame) > 0 &&

        // Push the updated amount
        amount.Update(ref frame, amount.Value(ref frame) - 1) 
        
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bind<A, B>(ref StackFrame frame) =>
        
        // Peek at the bind function
        frame.vars.Pop<Func<A, Iter<B>>>(out var f) &&

        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Push the mapped value on the stack
        // TODO: This should create a new source from f(a) than they yields all
        
        // Push the value
        frame.vars.Push(f(a))

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState forever<A>(ref StackFrame frame) =>
        
        // Peek at the forever value 
        frame.vars.Pop<A>(out var value) &&
        
        // Push the value
        frame.vars.Push(in value)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState singleton<A>(ref StackFrame frame) =>

        // The singleton should run only once, so check the `continue` flag.
        // If the `continue` flag is `true`, we run...
        globalM<bool>(ref frame, out var cont) && cont.Value(ref frame) &&

        // Peek at the singleton value 
        frame.vars.Pop<A>(out var value) &&

        // Set the `continue` flag to `false` so we don't run again
        cont.Update(ref frame, false) &&

        // Push the value
        frame.vars.Push(in value)

            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>

        // Peak-await the iterable instance
        frame.vars.Pop<K<T, A>>(out var ta) &&

        // Pop the iterable state
        globalM<IS>(ref frame, out var ts) &&

        // Step the iterable
        T.Next(in ta, ref ts.Value(ref frame), out var x) &&
        
        // Push the value
        frame.vars.Push(in x)

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iterator<A>(ref StackFrame frame) =>

        // Pop the iterator
        globalM<Iter<A>>(ref frame, out var ta) &&

        // Read the next value
        ta.Value(ref frame).TryGetValue(out var head, out var ta1) &&

        // Push the updated iterator
        ta.Update(ref frame, in ta1) &&
        
        // Push the value
        frame.vars.Push(in head)

            ? @continue(ref frame)
            : empty(ref frame);
}
