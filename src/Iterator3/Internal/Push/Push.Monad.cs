using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool flatten<A>(in StackFrame frame, in Iter<Iter<A>> ts) =>

        // Create a slot for the current iterator to go
        declare1(in frame, default(Iter<A>)) &&
        
        // Declare a slot for the iterators
        declare2(in frame, in ts) &&
        
        // Start the co-routine
        coroutine(in frame) &&
        
        // Load the 'current' iterator 
        ref1<Iter<A>>(in frame) &&
        
        // Load the sequence of iterators 
        ref2<Iter<Iter<A>>>(in frame) &&

        // Iterate over multiple iterators
        fun<A>(in frame, &Pull.flatten<A>, OpReturn.CanVoid);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool bind<A, B>(in StackFrame frame, in Iter<A> ta, in Func<A, Iter<B>> f) =>
        
        // Create a slot for the input iterator to go
        declare1(in frame, ta) &&
        
        // Create a slot for the bind result iterator to go
        declare2(in frame, default(Iter<B>)) &&
        
        // Start the co-routine
        coroutine(in frame) &&
        
        // Load the current iterator
        ref1<Iter<A>>(in frame) &&
        
        // Load the current bind result iterator 
        ref2<Iter<B>>(in frame) &&
        
        // Load the bind function
        arg3(in frame, in f) &&
        
        // Add the bind operation
        fun<B>(in frame, &Pull.bind<A, B>, OpReturn.CanVoid);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool bind<A, B>(in StackFrame frame, in Func<A, Iter<B>> f) =>
        
        // Create a slot for the bind result iterator to go
        declare2(in frame, default(Iter<B>)) &&
        
        // Start the co-routine
        coroutine(in frame) &&
        
        // Load the current bind result iterator 
        ref1<Iter<B>>(in frame) &&
        
        // Load the bind function
        arg2(in frame, in f) &&
        
        // Add the bind operation
        fun<B>(in frame, &Pull.bind<A, B>, OpReturn.CanVoid);    
}
