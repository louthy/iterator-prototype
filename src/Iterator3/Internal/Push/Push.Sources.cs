using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool forever<A>(ref StackFrame frame, in A value) =>

        yield(ref frame, in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool singleton<A>(ref StackFrame frame, in A value) =>
        
        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(value, out var ix) &&

        // Pull the value from the global and push it onto the 'vars' stack
        fun(ref frame, GlobalsGen<A>.pull(in ix));
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool iterableSetup<T, IS, A>(ref StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        
        // Push the iterable instance onto the globals-list
        declare1(ref frame, in ta) &&
        
        // Push a slot for the iterable state onto the globals-list
        declare2(ref frame, T.SetupImmutable(in ta));

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool iterable<T, IS, A>(ref StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Initialise the iterable state
        iterableSetup<T, IS, A>(ref frame, in ta) &&
        
        // Start the co-routine
        coroutine(ref frame) &&

        // Load the args
        ref1<K<T, A>>(ref frame) &&
 
        // Load the state
        ref2<IS>(ref frame) &&
        
        // Push iterable operation
        fun(ref frame, PullGen<A>.iterable<T, IS>()) &&
        
        // Fill the yield variable with the output of the iterable
        yield<A>(ref frame);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool iterator<A>(ref StackFrame frame, in Iter<A> ta) =>
        
        // Push the iterator
        declare1(ref frame, ta) &&
        
        // Start the co-routine
        coroutine(ref frame) &&
        
        // Push the iterator to the stack
        ref1<Iter<A>>(ref frame) &&

        // Push iterator operation
        fun(ref frame, &Pull.iterator<A>) &&
        
        // Fill the yield variable with the output of the iterator
        yield<A>(ref frame);
}
