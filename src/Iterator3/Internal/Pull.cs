using System.Diagnostics;
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static class Pull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState empty(ref StackFrame frame) =>
        frame.VoidCoRoutine()
            ? PullState.Continue
            : PullState.Void;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState @continue(ref StackFrame frame) =>
        PullState.Continue;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState coroutine<A>(ref StackFrame frame) =>
        frame.StartCoRoutine<A>() 
            ? @continue(ref frame)
            : empty(ref frame);

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
        
        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Peek at the map function
        frame.vars.Peek<Func<A, B>>(out var f) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(a))
        
            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState bind<A, B>(ref StackFrame frame) =>
        
        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Peek at the bind function
        frame.vars.Peek<Func<A, Iter<B>>>(out var f) &&

        // Push the mapped value on the stack
        // TODO: This should create a new source from f(a) than they yields all
        frame.vars.Push(f(a))
                
            ? yield<B>(ref frame)       
            : empty(ref frame);
    
    /// <summary>
    /// Ignore the input value `A` and return the constant value `*`. Where `*` is the
    /// type of the constant value on the stack.
    /// </summary>
    /// <remarks>
    /// <code>
    ///     * -> A -> * 
    /// </code>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState constant<A>(ref StackFrame frame) =>
        
        // Pop the non-constant value off the stack, and the constant value
        // should be left at the top. 
        frame.vars.Pop<A>()
                
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState singleton<A>(ref StackFrame frame) =>

        // The singleton should run only once, so check the `continue` flag.
        // If the `continue` flag is `true`, we run...
        frame.vars.Peek<bool>(out var cont) && cont

            // Pop the `continue` flag off the stack
            ? frame.vars.Pop<bool>() &&

              // Peek at the singleton value 
              frame.vars.Peek<A>(out var value) &&

              // Set the `continue` flag to `false`
              frame.vars.Push(false) 

                  // Create a new scope and yield the value
                  ? yield(ref frame, in value)
                  : empty(ref frame)

            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>

        // Pop the iterable state
        frame.yields.Pop<IS>(out var ts) &&

        // Peak-await the iterable instance
        frame.yields.Peek<K<T, A>>(out var ta) &&

        // Step the iterable
        T.StepImmutable(in ta, in ts, out var x, out ts) &&

        // Push the iterable state
        frame.yields.Push(in ts)

            ? yield(ref frame, in x)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iter<A>(ref StackFrame frame) =>

        // Pop the iterator
        frame.vars.Pop<Iter<A>>(out var iter) &&

        // Read the next value
        iter.TryGetValue(out var head, out iter)

            // Push the updated iterator
            ? frame.vars.Push(in iter) 
                  
                  // Yield the value
                  ? yield(ref frame, in head)
                  : empty(ref frame)
                  
            : empty(ref frame);
}
