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
            ? PullState.Void
            : PullState.Continue;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState @continue(ref StackFrame frame) =>
        PullState.Continue;

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
    public static bool global<A>(ref StackFrame frame, out A global)
    {
        if(frame.vars.Pop<Global<A>>(out var g))
        {
            global = g.Value(ref frame);
            return true;
        }
        else
        {
            global = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool global<A>(ref StackFrame frame) =>
        frame.vars.Pop<Global<A>>(out var g) &&
        frame.vars.Push(in g.Value(ref frame));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState arg<A>(ref StackFrame frame) =>

        // Load arg index
        frame.vars.Pop<ushort>(out var index) &&
        
        // Load the arg
        frame.args.At<A>(in index, out var c) &&
        
        // Push it onto the variables-stack
        frame.vars.Push(in c)
            
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState arg<A>(ref StackFrame frame, in ushort id) =>
        
        // Load the arg
        frame.args.At<A>(in id, out var c) &&
        
        // Push it onto the variables-stack
        frame.vars.Push(in c)
            
            ? @continue(ref frame)
            : empty(ref frame);

    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState dup<A>(ref StackFrame frame) =>
        
        // Peek the top value
        frame.vars.Peek<A>(out var value) &&
        
        // Duplicate it on the stack
        frame.vars.Push(in value) 

            ? @continue(ref frame)
            : empty(ref frame);
            */
            

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
        global<Func<A, B>>(ref frame, out var f) &&

        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Push the mapped value on the stack
        frame.vars.Push(f(a))
        
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
        global<Func<A, Iter<B>>>(ref frame, out var f) &&

        // Take the value off the stack
        frame.vars.Pop<A>(out var a) &&

        // Push the mapped value on the stack
        // TODO: This should create a new source from f(a) than they yields all
        frame.vars.Push(f(a))
                
            ? yield<B>(ref frame)       
            : empty(ref frame);
    
    /// <summary>
    /// Ignore the input value `A` and return the constant value `C`. Where `C` is the
    /// type of the constant value on the stack.
    /// </summary>
    /// <remarks>
    /// <code>
    ///     C -> A -> C 
    /// </code>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState constant<A, C>(ref StackFrame frame) =>
        
        // Put the constant value on the stack
        global<C>(ref frame, out var c) &&
        
        // Pop the non-constant value off the stack
        frame.vars.Pop<A>() && 
        
        frame.vars.Push(in c)
                
            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState forever<A>(ref StackFrame frame) =>
        
        // Peek at the forever value 
        global<A>(ref frame, out var value)
        
            // Yield the value downstream
            ? yield(ref frame, in value)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState singleton<A>(ref StackFrame frame) =>

        // The singleton should run only once, so check the `continue` flag.
        // If the `continue` flag is `true`, we run...
        globalM<bool>(ref frame, out var cont) && cont.Value(ref frame) &&

        // Peek at the singleton value 
        global<A>(ref frame, out var value) &&

        // Set the `continue` flag to `false` so we don't run again
        cont.Update(ref frame, false)

            // Yield the value downstream
            ? yield(ref frame, in value)
            : empty(ref frame);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>

        // Pop the iterable state
        globalM<IS>(ref frame, out var ts) &&

        // Peak-await the iterable instance
        global<K<T, A>>(ref frame, out var ta) &&

        // Step the iterable
        T.StepImmutable(in ta, in ts.Value(ref frame), out var x, out var ts1) &&

        // Push the iterable state
        ts.Update(ref frame, in ts1)

            ? yield(ref frame, in x)
            : empty(ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState iter<A>(ref StackFrame frame) =>

        // Pop the iterator
        globalM<Iter<A>>(ref frame, out var iter) &&

        // Read the next value
        iter.Value(ref frame).TryGetValue(out var head, out var iter1) &&

        // Push the updated iterator
        iter.Update(ref frame, in iter1)

            // Yield the value
            ? yield(ref frame, in head)
            : empty(ref frame);
}
