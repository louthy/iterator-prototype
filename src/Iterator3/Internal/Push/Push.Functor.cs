using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.map<A, B>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool bimap<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.bimap<A, B, C>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool bimap1<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.bimap1<A, B, C>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool trimap<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.trimap<A, B, C, D>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool trimap1<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.trimap1<A, B, C, D>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool quadmap<A, B, C, D, E>(ref StackFrame frame, in Func<A, B, C, D, E> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.quadmap<A, B, C, D, E>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool quadmap1<A, B, C, D, E>(ref StackFrame frame, in Func<A, B, C, D, E> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.quadmap1<A, B, C, D, E>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pentamap<A, B, C, D, E, F>(ref StackFrame frame, in Func<A, B, C, D, E, F> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.pentamap<A, B, C, D, E, F>);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pentamap1<A, B, C, D, E, F>(ref StackFrame frame, in Func<A, B, C, D, E, F> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.pentamap1<A, B, C, D, E, F>);
}
