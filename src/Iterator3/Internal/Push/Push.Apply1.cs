using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.Default)]
    internal static bool apply1<A, B, C>(in StackFrame frame, Func<A, B, C> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun(in frame, &Pull.apply1<A, B, C>);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply1<A, B, C, D>(in StackFrame frame, Func<A, B, C, D> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun(in frame, &Pull.apply1<A, B, C, D>);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply1<A, B, C, D, E>(in StackFrame frame, Func<A, B, C, D, E> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun(in frame, &Pull.apply1<A, B, C, D, E>);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply1<A, B, C, D, E, F>(in StackFrame frame, Func<A, B, C, D, E, F> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun(in frame, &Pull.apply1<A, B, C, D, E, F>);    
        
    [MethodImpl(Optimisations.Default)]
    internal static bool apply1<A, B, C, D, E, F, G>(in StackFrame frame, Func<A, B, C, D, E, F, G> f) =>
        
        arg1(in frame, f) &&
        
        // Push apply operation
        fun(in frame, &Pull.apply1<A, B, C, D, E, F, G>);
}
