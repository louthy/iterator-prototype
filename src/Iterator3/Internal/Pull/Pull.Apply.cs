#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C>(in StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C>>(in frame,out var f) && 
        
        // Pop the second element
        pop<B>(in frame, out var b) &&

        // Peek the first element
        peek<A>(in frame, out var a) && 

        // Push the tuple
        @return(in frame, f(a, b)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D>(in StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D>>(in frame,out var f) && 

        // Pop the third element
        pop<C>(in frame, out var c) &&

        // Pop the second element
        pop<B>(in frame, out var b) && 

        // Peek the first element
        peek<A>(in frame, out var a) && 

        // Re-push the second element
        push(in frame, in b) && 

        // Push the tuple
        @return(in frame, f(a, b, c)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D, E>(in StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D, E>>(in frame,out var f) && 

        // Pop the fourth element
        pop<D>(in frame, out var d) &&

        // Pop the third element
        pop<C>(in frame, out var c) &&

        // Pop the second element
        pop<B>(in frame, out var b) && 

        // Peek the first element
        peek<A>(in frame, out var a) && 

        // Re-push the second element
        push(in frame, in b) && 

        // Re-push the second element
        push(in frame, in c) && 

        // Push the tuple
        @return(in frame, f(a, b, c, d)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D, E, F>(in StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D, E, F>>(in frame,out var f) && 

        // Pop the fifth element
        pop<E>(in frame, out var e) &&

        // Pop the fourth element
        pop<D>(in frame, out var d) &&

        // Pop the third element
        pop<C>(in frame, out var c) &&

        // Pop the second element
        pop<B>(in frame, out var b) && 

        // Peek the first element
        peek<A>(in frame, out var a) && 

        // Re-push the second element
        push(in frame, in b) && 

        // Re-push the second element
        push(in frame, in c) && 

        // Re-push the third element
        push(in frame, in d) && 

        // Push the tuple
        @return(in frame, f(a, b, c, d, e)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply<A, B, C, D, E, F, G>(in StackFrame frame) =>

        // TODO: Stack 'from top' indexer to avoid all these pushes and pops
        
        // Load the apply function
        arg1<Func<A, B, C, D, E, F, G>>(in frame,out var fun) && 

        // Pop the sixth element
        pop<F>(in frame, out var f) &&

        // Pop the fifth element
        pop<E>(in frame, out var e) &&

        // Pop the fourth element
        pop<D>(in frame, out var d) &&

        // Pop the third element
        pop<C>(in frame, out var c) &&

        // Pop the second element
        pop<B>(in frame, out var b) && 

        // Peek the first element
        peek<A>(in frame, out var a) && 

        // Re-push the second element
        push(in frame, in b) && 

        // Re-push the second element
        push(in frame, in c) && 

        // Re-push the third element
        push(in frame, in d) && 

        // Re-push the fourth element
        push(in frame, in e) && 

        // Push the tuple
        @return(in frame, fun(a, b, c, d, e, f)) 

            ? @continue(in frame)
            : empty(in frame);    
}