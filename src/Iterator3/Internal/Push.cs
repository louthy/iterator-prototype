using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool pure<A>(ref StackFrame frame) =>

        // Push the yield operation
        frame.Add(&Pull.pure<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool pure<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant value
        @const(ref frame, in value) &&
        
        // Push the yield operation
        frame.Add(&Pull.pure<A>);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame) =>

        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(default(A), out var yieldIx) &&
        
        // Fill the yield variable with the output of whatever ran before us
        frame.Add(G.push<A>(in yieldIx)) &&

        // Flag that this co-routine has yielded something
        frame.Add(&Pull.yield) &&
        
        // Start a new co-routine for the value
        frame.Add(&Pull.coroutine) &&
    
        // Pull the value from the global and push it onto the 'vars' stack
        frame.Add(G.pull<A>(in yieldIx));
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame, in A value) =>

        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(value, out var yieldIx) &&

        // Flag that this co-routine has yielded something
        frame.Add(&Pull.yield) &&
        
        // Start a new co-routine for the value
        frame.Add(&Pull.coroutine) &&
        
        // Pull the value from the global and push it onto the 'vars' stack
        frame.Add(G.pull<A>(in yieldIx));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool dup<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.dup<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool coroutine(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        frame.Add(&Pull.coroutine);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool scope(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        frame.Prepend(&Pull.coroutine);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool await<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.await<A>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        frame.Add(&Pull.map<A, B>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bimap<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        frame.Add(&Pull.bimap<A, B, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bimap1<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        frame.Add(&Pull.bimap1<A, B, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool trimap<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        frame.Add(&Pull.trimap<A, B, C, D>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool trimap1<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        @const(ref frame, in f) &&
        
        // Add the map operation
        frame.Add(&Pull.trimap1<A, B, C, D>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bind<A, B>(ref StackFrame frame, in Func<A, Iter<B>> f) =>
        
        // Push the bind function
        @const(ref frame, in f) &&
        
        // Add the bind operation
        frame.Add(&Pull.bind<A, B>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool take(ref StackFrame frame, in int amount) =>

        // Push the amount
        var(ref frame, amount) &&
        
        // Push take operation
        frame.Add(&Pull.take);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static PullState apply<A, B>(ref StackFrame frame) =>
        
        // Push apply operation
        frame.Add(&Pull.apply<A, B>);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B>(ref StackFrame frame) => 
        
        // Push tuple operation
        frame.Add(&Pull.tuple<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B, C>(ref StackFrame frame) => 
        
        // Push tuple operation
        frame.Add(&Pull.tuple<A, B, C>);    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool elements<A, B>(ref StackFrame frame) => 
        
        // Push elements operation
        frame.Add(&Pull.elements<A, B>);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool elements<A, B, C>(ref StackFrame frame) => 
        
        // Push elements operation
        frame.Add(&Pull.elements<A, B, C>);    
}
