using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool pure<A>(ref StackFrame frame) =>

        // Push the yield operation
        fun(ref frame, &Pull.pure<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool pure<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant value
        @const(ref frame, in value) &&
        
        // Push the yield operation
        fun(ref frame, &Pull.pure<A>);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame) =>

        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(default(A), out var yieldIx) &&
        
        // Fill the yield variable with the output of whatever ran before us
        fun(ref frame, G.yield<A>(in yieldIx));
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame, in A value) =>

        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(value, out var yieldIx) &&
        
        // Fill the yield variable with the output of whatever ran before us
        fun(ref frame, G.yield<A>(in yieldIx));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool dup<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        fun(ref frame, &Pull.dup<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool fun(ref StackFrame frame, in delegate*<ref StackFrame, PullState> f) =>
        frame.Add(f);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool coroutine(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(ref frame, &Pull.coroutine);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool scope(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        frame.Prepend(&Pull.coroutine);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool await<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        fun(ref frame, &Pull.await<A>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.map<A, B>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bimap<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.bimap<A, B, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bimap1<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.bimap1<A, B, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool trimap<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.trimap<A, B, C, D>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool trimap1<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.trimap1<A, B, C, D>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bind<A, B>(ref StackFrame frame, in Func<A, Iter<B>> f) =>
        
        // Push the bind function
        @const(ref frame, in f) &&
        
        // Add the bind operation
        fun(ref frame, &Pull.bind<A, B>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool take(ref StackFrame frame, in int amount) =>

        // Push the amount
        var(ref frame, amount) &&
        
        // Push take operation
        fun(ref frame, &Pull.take);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static PullState apply<A, B, C>(ref StackFrame frame, Func<A, B, C> f) =>
        
        @const(ref frame, f) &&
        
        // Push apply operation
        fun(ref frame, &Pull.apply<A, B, C>);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static PullState apply<A, B, C, D>(ref StackFrame frame, Func<A, B, C, D> f) =>
        
        @const(ref frame, f) &&
        
        // Push apply operation
        fun(ref frame, &Pull.apply<A, B, C, D>);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B>(ref StackFrame frame) => 
        
        // Push tuple operation
        fun(ref frame, &Pull.tuple<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B, C>(ref StackFrame frame) => 
        
        // Push tuple operation
        fun(ref frame, &Pull.tuple<A, B, C>);    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool elements<A, B>(ref StackFrame frame) => 
        
        // Push elements operation
        fun(ref frame, &Pull.elements<A, B>);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool elements<A, B, C>(ref StackFrame frame) => 
        
        // Push elements operation
        fun(ref frame, &Pull.elements<A, B, C>);    
}
