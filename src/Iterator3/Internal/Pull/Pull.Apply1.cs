#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C>(in StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C>>(in frame,out var f) && 

        // Pop the next item to apply
        pop<B>(in frame, out var next) &&
        
        // Peek at item 1
        peek<A>(in frame, out var tuple) &&

        // Push the tuple
        @return(in frame, f(tuple, next)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D>(in StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D>>(in frame,out var f) && 
        
        // Pop the next item to apply
        pop<C>(in frame, out var next) &&

        // Peek at the tuple
        peek<(A, B)>(in frame, out var tuple) &&

        // Push the tuple
        @return(in frame, f(tuple.Item1, tuple.Item2, next)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D, E>(in StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D, E>>(in frame,out var f) && 

        // Pop the next item to apply
        pop<D>(in frame, out var next) &&
        
        // Peek the tuple
        peek<(A, B, C)>(in frame, out var tuple) &&

        // Push the tuple
        @return(in frame, f(tuple.Item1, tuple.Item2, tuple.Item3, next)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D, E, F>(in StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D, E, F>>(in frame,out var f) && 

        // Pop the next item to apply
        pop<E>(in frame, out var next) &&
        
        // Peek the tuple
        peek<(A, B, C, D)>(in frame, out var tuple) &&

        // Push the tuple
        @return(in frame, f(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, next)) 

            ? @continue(in frame)
            : empty(in frame);

    [MethodImpl(Optimisations.Default)]
    public static int apply1<A, B, C, D, E, F, G>(in StackFrame frame) =>

        // Load the apply function
        arg1<Func<A, B, C, D, E, F, G>>(in frame,out var f) && 

        // Pop the next item to apply
        pop<F>(in frame, out var next) &&
        
        // Peek the tuple
        peek<(A, B, C, D, E)>(in frame, out var tuple) &&

        // Push the tuple
        @return(in frame, f(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, next)) 

            ? @continue(in frame)
            : empty(in frame);    
}