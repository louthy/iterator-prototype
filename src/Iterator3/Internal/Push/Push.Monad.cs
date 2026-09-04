using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool flatten<A>(ref StackFrame frame, in Iter<Iter<A>> ts) =>

        // Create a slot for the current iterator to go
        declare1(ref frame, default(Iter<A>)) &&
        
        // Declare a slot for the iterators
        declare2(ref frame, in ts) &&
        
        // Start the co-routine
        coroutine(ref frame) &&
        
        // Load the 'current' iterator 
        ref1<Iter<A>>(ref frame) &&
        
        // Load the sequence of iterators 
        ref2<Iter<Iter<A>>>(ref frame) &&

        // Iterate over multiple iterators
        fun(ref frame, &Pull.flatten<A>) &&
            
        // Fill the yield variable with the output of the iterator
        yield<A>(ref frame);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool bind<A, B>(ref StackFrame frame, in Iter<A> ta, in Func<A, Iter<B>> f) =>
        
        // Create a slot for the input iterator to go
        declare1(ref frame, ta) &&
        
        // Create a slot for the bind result iterator to go
        declare2(ref frame, default(Iter<B>)) &&
        
        // Start the co-routine
        coroutine(ref frame) &&
        
        // Load the current iterator
        ref1<Iter<A>>(ref frame) &&
        
        // Load the current bind result iterator 
        ref2<Iter<B>>(ref frame) &&
        
        // Load the bind function
        arg3(ref frame, in f) &&
        
        // Add the bind operation
        fun(ref frame, &Pull.bind<A, B>) &&
            
        // Fill the yield variable with the output of the iterator
        yield<A>(ref frame);
}
