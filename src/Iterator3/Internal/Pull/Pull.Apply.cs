#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C>(ref StackFrame frame) =>

        // Load the apply function
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

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
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
    public static int apply<A, B, C, D, E>(ref StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D, E>>(ref frame,out var f) && 

        // Pop the fourth element
        pop<D>(ref frame, out var d) &&

        // Pop the third element
        pop<C>(ref frame, out var c) &&

        // Pop the second element
        pop<B>(ref frame, out var b) && 

        // Peek the first element
        peek<A>(ref frame, out var a) && 

        // Re-push the second element
        push(ref frame, in b) && 

        // Re-push the second element
        push(ref frame, in c) && 

        // Push the tuple
        @return(ref frame, f(a, b, c, d)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D, E, F>(ref StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D, E, F>>(ref frame,out var f) && 

        // Pop the fifth element
        pop<E>(ref frame, out var e) &&

        // Pop the fourth element
        pop<D>(ref frame, out var d) &&

        // Pop the third element
        pop<C>(ref frame, out var c) &&

        // Pop the second element
        pop<B>(ref frame, out var b) && 

        // Peek the first element
        peek<A>(ref frame, out var a) && 

        // Re-push the second element
        push(ref frame, in b) && 

        // Re-push the second element
        push(ref frame, in c) && 

        // Re-push the third element
        push(ref frame, in d) && 

        // Push the tuple
        @return(ref frame, f(a, b, c, d, e)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D, E, F, G>(ref StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D, E, F, G>>(ref frame,out var fun) && 

        // Pop the sixth element
        pop<F>(ref frame, out var f) &&

        // Pop the fifth element
        pop<E>(ref frame, out var e) &&

        // Pop the fourth element
        pop<D>(ref frame, out var d) &&

        // Pop the third element
        pop<C>(ref frame, out var c) &&

        // Pop the second element
        pop<B>(ref frame, out var b) && 

        // Peek the first element
        peek<A>(ref frame, out var a) && 

        // Re-push the second element
        push(ref frame, in b) && 

        // Re-push the second element
        push(ref frame, in c) && 

        // Re-push the third element
        push(ref frame, in d) && 

        // Re-push the fourth element
        push(ref frame, in e) && 

        // Push the tuple
        @return(ref frame, fun(a, b, c, d, e, f)) 

            ? @continue(ref frame)
            : empty(ref frame);    
}