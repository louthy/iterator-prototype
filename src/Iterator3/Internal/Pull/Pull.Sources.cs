using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int iterator<A>(ref StackFrame frame) =>

        // Pop the iterator
        arg1<Iter<A>>(ref frame, out var ta) &&
        
        // Read the next value
        ta.TryGetValue(out var x, out var xs) &&

        // Push the updated iterator
        update1(ref frame, in xs) &&

        // Return the value
        @return(ref frame, in x) 

            ? @continue(ref frame)
            : empty(ref frame);
    
    [MethodImpl(Optimisations.Default)]
    public static int iterators<A>(ref StackFrame frame) =>

        // Pop the iterators
        arg1<Arr<Iter<A>>>(ref frame, out var ts) &&
        
        // Pp the index
        arg2<int>(ref frame, out var ix) && ix < ts.Count &&
        
        // Read the next value
        ts[ix].TryGetValue(out var x, out var xs) &&

        // Push the updated iterator
        // We use an array here, because concat would normally not be run a huge number of
        // times, so the array is likely small and cheap to copy.
        // TODO: Consider other approaches that don't require allocations 
        update1(ref frame, ts.SetItem(ix, in xs)) &&

        // Return the value
        @return(ref frame, in x) 

            ? @continue(ref frame)
            : empty(ref frame);    
}
