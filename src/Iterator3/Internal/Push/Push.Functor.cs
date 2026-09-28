using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool map<A, B>(in StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        arg1(in frame, in f) &&
        
        // Add the map operation
        fun<A, B>(in frame, PullGen<A, B>.map, OpReturn.Default);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool bimap<A, B, C>(in StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        arg1(in frame, in f) &&
        
        // Add the map operation
        fun<A, B, C>(in frame, PullGen<A, B, C>.bimap, OpReturn.Default);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool trimap<A, B, C, D>(in StackFrame frame, in Func<A, B, C, D> f) =>

        // Push the mapping function
        arg1(in frame, in f) &&

        // Add the map operation
        fun<A, B, C, D>(in frame, &Pull.trimap<A, B, C, D>, OpReturn.Default);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool quadmap<A, B, C, D, E>(in StackFrame frame, in Func<A, B, C, D, E> f) =>
        
        // Push the mapping function
        arg1(in frame, in f) &&
        
        // Add the map operation
        fun<A, B, C, D, E>(in frame, &Pull.quadmap<A, B, C, D, E>, OpReturn.Default);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pentamap<A, B, C, D, E, F>(in StackFrame frame, in Func<A, B, C, D, E, F> f) =>
        
        // Push the mapping function
        arg1(in frame, in f) &&
        
        // Add the map operation
        fun<A, B, C, D, E, F>(in frame, &Pull.pentamap<A, B, C, D, E, F>, OpReturn.Default);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool sextamap<A, B, C, D, E, F, G>(in StackFrame frame, in Func<A, B, C, D, E, F, G> f) =>
        
        // Push the mapping function
        arg1(in frame, in f) &&
        
        // Add the map operation
        fun<A, B, C, D, E, F, G>(in frame, &Pull.sextamap<A, B, C, D, E, F, G>, OpReturn.Default);
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool septamap<A, B, C, D, E, F, G, H>(in StackFrame frame, in Func<A, B, C, D, E, F, G, H> f) =>
        
        // Push the mapping function
        arg1(in frame, in f) &&
        
        // Add the map operation
        fun<A, B, C, D, E, F, G, H>(in frame, &Pull.septamap<A, B, C, D, E, F, G, H>, OpReturn.Default);
}
