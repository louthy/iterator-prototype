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
    public static bool global<A>(ref StackFrame frame, in A value) =>

        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // The operation to load the global has the index built-in
        frame.Add(G.pull<A>(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool arg<A>(ref StackFrame frame, in A value) =>

        // Push the value to the args-list
        frame.args.Add(in value, out var ix) &&

        // The operation to load the global has the index built-in
        frame.Add(Arg.pull<A>(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool yield<A>(ref StackFrame frame, in A value) =>
        
        // Add the constant value
        global(ref frame, in value) &&
        
        // Push the yield operation
        frame.Add(&Pull.yield<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool pure<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        frame.Add(&Pull.pure<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool pure<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant value
        global(ref frame, in value) &&
        
        // Push the yield operation
        frame.Add(&Pull.pure<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constant<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant state value on to the stack
        global(ref frame, in value) &&
        
        // Add the constant operation
        frame.Add(&Pull.constant<A, A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constant<A, C>(ref StackFrame frame, in A value) =>
        
        // Push the constant state value on to the stack
        global(ref frame, in value) &&
        
        // Add the constant operation
        frame.Add(&Pull.constant<A, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        global(ref frame, in f) &&
        
        // Add the map operation
        frame.Add(&Pull.map<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool bind<A, B>(ref StackFrame frame, in Func<A, Iter<B>> f) =>
        
        // Push the bind function
        global(ref frame, in f) &&
        
        // Add the bind operation
        frame.Add(&Pull.bind<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool forever<A>(ref StackFrame frame, in A value) =>
        
        // Push the forever value
        global(ref frame, in value) &&
        
        // Push forever operation
        frame.Add(&Pull.forever<A>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool singleton<A>(ref StackFrame frame, in A value) =>
        
        // Push the singleton value
        global(ref frame, in value) &&
        
        // Push the flag that indicates we should yield a value
        global(ref frame, true) &&
        
        // Push singleton operation
        frame.Add(&Pull.singleton<A>);
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool iterable<T, IS, A>(ref StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Push the iterable state onto the constants-list
        global(ref frame, T.SetupImmutable(in ta)) &&
    
        // Push the iterable instance onto the constants-list
        global(ref frame, in ta) &&
        
        // Push the yield operation
        frame.Add(&Pull.iterable<T, IS, A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool iter<A>(ref StackFrame frame, in Iter<A> other) =>

        // Push the other to the stack (this will box the Iter structure)
        global(ref frame, in other) &&

        // Push coroutine program-counter
        frame.Add(&Pull.iter<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool take(ref StackFrame frame, in int amount) =>

        // Push the amount
        global(ref frame, amount) &&
        
        // Push take operation
        frame.Add(&Pull.take);
}
