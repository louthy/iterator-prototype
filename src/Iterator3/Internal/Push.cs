using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

static unsafe class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool yield<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.yield<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool await<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.await<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool yield<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant value
        push(ref frame, in value) &&
        
        // Push the yield operation
        frame.Add(&Pull.yield<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool pure<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.pure<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool pure<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant value
        push(ref frame, in value) &&
        
        // Push the yield operation
        frame.Add(&Pull.pure<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constant<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant state value on to the stack
        frame.AddArg(in value) &&
        
        // Add the constant operation
        frame.Add(&Pull.constant<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constant<A, B>(ref StackFrame frame, in A value) =>
        
        // Push the constant state value on to the stack
        frame.AddArg(in value) &&
        
        // Add the constant operation
        frame.Add(&Pull.constant<B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool push<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant state value on to the stack
        frame.AddArg(in value);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        frame.AddArg(in f) &&
        
        // Add the map operation
        frame.Add(&Pull.map<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool bind<A, B>(ref StackFrame frame, in Func<A, Iter<B>> f) =>
        
        // Push the bind function
        frame.AddArg(in f) &&
        
        // Add the bind operation
        frame.Add(&Pull.bind<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool forever<A>(ref StackFrame frame) =>
                
        // Push a duplicate of the top value 
        frame.Add(&Pull.dup<A>) &&
        
        // Yield 
        yield<A>(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool forever<A>(ref StackFrame frame, in A value) =>
        
        // Push the forever value
        frame.AddArg(in value) &&
        
        // Repeat it forever 
        forever<A>(ref frame);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool singleton<A>(ref StackFrame frame, in A value) =>
        
        // Push the singleton value
        frame.AddArg(true) &&
        
        // Push the singleton value
        frame.AddArg(in value) &&
        
        // Push subroutine
        frame.Add(&Pull.singleton<A>);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool iterable<T, IS, A>(ref StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Push the iterable instance onto the stack
        singleton(ref frame, in ta) &&
        
        // Yield the state
        singleton(ref frame, T.SetupImmutable(in ta)) &&
    
        // Push the yield operation
        frame.Add(&Pull.iterable<T, IS, A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool iter<A>(ref StackFrame frame, in Iter<A> other) =>

        // Push the other to the stack (this will box the Iter structure)
        frame.AddArg(in other) &&

        // Push coroutine program-counter
        frame.Add(&Pull.iter<A>);

}
