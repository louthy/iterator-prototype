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
        fun<A>(in frame, GlobalsGen<A>.pull(ix), OpReturn.Default);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool iterable<T, IS, A>(in StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Initialise the iterable state
        declare(in frame, in ta, T.SetupImmutable(in ta)) &&
        
        // Start the co-routine
        coroutine(in frame) &&

        // Load the args
        ref1<K<T, A>>(in frame) &&
 
        // Load the state
        ref2<IS>(in frame) &&
        
        // Push iterable operation
        fun<A>(in frame, PullGen<A>.iterable<T, IS>(), OpReturn.CanVoidOrSkip);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool iterator<A>(in StackFrame frame, in Iter<A> ta) =>

        // Push the iterator
        declare1(in frame, ta) &&

        // Start the co-routine
        coroutine(in frame) &&

        // Push the iterator to the stack
        ref1<Iter<A>>(in frame) &&

        // Push iterator operation
        fun<A>(in frame, PullGen<A>.iterator, OpReturn.CanVoidOrSkip);
    
    public static bool lazy<A>(in StackFrame frame, in Func<Iter<A>> f) =>

        // Push the slot for the acquired iterator
        declare1(in frame, default(Iter<A>)) &&

        // Push the lazy iterator 
        declare2(in frame, f) &&

        // Push the slot for the flag indicating whether the iterator has been acquired
        declare3(in frame, false) &&

        // Start the co-routine
        coroutine(in frame) &&

        // Push the iterator to the stack
        ref1<Iter<A>>(in frame) &&

        // Push the lazy iterator to the stack
        ref2<Func<Iter<A>>>(in frame) &&

        // Push the flag indicating whether the iterator has been acquired
        ref3<bool>(in frame) &&

        // Push iterator operation
        fun<A>(in frame, PullGen<A>.lazyIterator, OpReturn.CanVoidOrSkip);

}
