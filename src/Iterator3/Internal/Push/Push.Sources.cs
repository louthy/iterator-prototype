using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool forever<A>(in StackFrame frame, in A value) =>

        yield(in frame, in value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool singleton<A>(in StackFrame frame, in A value) =>
        
        // Create a global variable, this will be the storage for our yield value
        frame.globals.AddMutable(value, out var ix) &&

        // Pull the value from the global and push it onto the 'vars' stack
        fun(in frame, GlobalsGen<A>.pull(ix));
        
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool iterableSetup<T, IS, A>(in StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        
        // Push the iterable instance onto the globals-list
        const1(in frame, in ta) &&
        
        // Push a slot for the iterable state onto the globals-list
        declare2(in frame, T.SetupImmutable(in ta));

    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool iterable<T, IS, A>(in StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Initialise the iterable state
        iterableSetup<T, IS, A>(in frame, in ta) &&
        
        // Start the co-routine
        coroutine(in frame) &&

        // Load the args
        ref1<K<T, A>>(in frame) &&
 
        // Load the state
        ref2<IS>(in frame) &&
        
        // Push iterable operation
        fun(in frame, PullGen<A>.iterable<T, IS>()) &&
        
        // Fill the yield variable with the output of the iterable
        yield<A>(in frame);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool iterator<A>(in StackFrame frame, in Iter<A> ta) =>
        
        // Push the iterator
        const1(in frame, ta) &&
        
        // Start the co-routine
        coroutine(in frame) &&
        
        // Push the iterator to the stack
        ref1<Iter<A>>(in frame) &&

        // Push iterator operation
        fun(in frame, PullGen<A>.iterator) &&
        
        // Fill the yield variable with the output of the iterator
        yield<A>(in frame);

}
