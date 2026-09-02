#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C>(ref StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C>>(ref frame,out var f) && 

        // Pop the next item to apply
        pop<B>(ref frame, out var next) &&
        
        // Peek at item 1
        peek<A>(ref frame, out var tuple) &&

        // Push the tuple
        @return(ref frame, f(tuple, next)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D>(ref StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D>>(ref frame,out var f) && 
        
        // Pop the next item to apply
        pop<C>(ref frame, out var next) &&

        // Peek at the tuple
        peek<(A, B)>(ref frame, out var tuple) &&

        // Push the tuple
        @return(ref frame, f(tuple.Item1, tuple.Item2, next)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D, E>(ref StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D, E>>(ref frame,out var f) && 

        // Pop the next item to apply
        pop<D>(ref frame, out var next) &&
        
        // Peek the tuple
        peek<(A, B, C)>(ref frame, out var tuple) &&

        // Push the tuple
        @return(ref frame, f(tuple.Item1, tuple.Item2, tuple.Item3, next)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D, E, F>(ref StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D, E, F>>(ref frame,out var f) && 

        // Pop the next item to apply
        pop<E>(ref frame, out var next) &&
        
        // Peek the tuple
        peek<(A, B, C, D)>(ref frame, out var tuple) &&

        // Push the tuple
        @return(ref frame, f(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, next)) 

            ? @continue(ref frame)
            : empty(ref frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D, E, F, G>(ref StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D, E, F, G>>(ref frame,out var f) && 

        // Pop the next item to apply
        pop<F>(ref frame, out var next) &&
        
        // Peek the tuple
        peek<(A, B, C, D, E)>(ref frame, out var tuple) &&

        // Push the tuple
        @return(ref frame, f(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, next)) 

            ? @continue(ref frame)
            : empty(ref frame);    
}