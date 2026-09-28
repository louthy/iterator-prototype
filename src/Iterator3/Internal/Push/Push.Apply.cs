#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C>(in StackFrame frame, Func<A, B, C> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun<A, B, C>(in frame, &Pull.apply<A, B, C>, OpReturn.Default);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C, D>(in StackFrame frame, Func<A, B, C, D> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun<A, B, C, D>(in frame, &Pull.apply<A, B, C, D>, OpReturn.Default);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C, D, E>(in StackFrame frame, Func<A, B, C, D, E> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun<A, B, C, D, E>(in frame, &Pull.apply<A, B, C, D, E>, OpReturn.Default);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C, D, E, F>(in StackFrame frame, Func<A, B, C, D, E, F> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun<A, B, C, D, E, F>(in frame, &Pull.apply<A, B, C, D, E, F>, OpReturn.Default);    
        
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C, D, E, F, G>(in StackFrame frame, Func<A, B, C, D, E, F, G> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun<A, B, C, D, E, G>(in frame, &Pull.apply<A, B, C, D, E, F, G>, OpReturn.Default);
}
