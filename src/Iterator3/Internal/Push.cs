using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

static unsafe class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool coroutine(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        frame.Add(&Pull.coroutine);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool coroutine<A>(ref StackFrame frame) =>
        
        // Push the single-arg coroutine operation
        frame.Add(&Pull.coroutine<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.yield<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool await<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.await<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool @const<A>(ref StackFrame frame, in A value) =>

        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // The operation to load the global has the index built-in
        frame.Add(G.pull<A>(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool var<A>(ref StackFrame frame, in A value) =>

        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // The operation to load the global has the index built-in
        frame.Add(G.pullM<A>(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame, in A value) =>
        
        // Add the constant value
        @const(ref frame, in value) &&
        
        // Push the yield operation
        frame.Add(&Pull.yield<A>);
    
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
    public static bool forever<A>(ref StackFrame frame, in A value) =>

        // Mark the start of this co-routine
        coroutine(ref frame) &&

        // Push the forever value
        @const(ref frame, in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool singleton<A>(ref StackFrame frame, in A value) =>

        // Push the singleton value
        @const(ref frame, in value) &&
        
        // Repeat only once
        take(ref frame, 1);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool iterable<T, IS, A>(ref StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Mark the start of this co-routine
        coroutine(ref frame) &&
        
        // Push the iterable state onto the globals-list
        var(ref frame, T.SetupImmutable(in ta)) &&
    
        // Push the iterable instance onto the globals-list
        @const(ref frame, in ta) &&
        
        // Push the yield operation
        frame.Add(&Pull.iterable<T, IS, A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool iterator<A>(ref StackFrame frame, in Iter<A> ta) =>

        // Mark the start of this co-routine
        coroutine(ref frame) &&
        
        // Push the iterator to the stack (TODO: this will box the Iter structure!)
        var(ref frame, in ta) &&

        // Push coroutine program-counter
        frame.Add(&Pull.iterator<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool take(ref StackFrame frame, in int amount) =>

        // Push the amount
        var(ref frame, amount) &&
        
        // Push take operation
        frame.Add(&Pull.take);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B>(ref StackFrame frame) => 
        
        // Push tuple operation
        frame.Add(&Pull.tuple<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B, C>(ref StackFrame frame) => 
        
        // Push tuple operation
        frame.Add(&Pull.tuple<A, B, C>);    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple1<A, B, C>(ref StackFrame frame) => 
        
        // Push tuple operation
        frame.Add(&Pull.tuple1<A, B, C>);    
}
