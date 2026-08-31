using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static PullState iterator<A>(ref StackFrame frame) =>

        // Pop the iterator
        arg<Iter<A>>(ref frame, out var ta, out var g) &&
        
        // Read the next value
        ta.TryGetValue(out var x, out var xs) &&

        // Push the updated iterator
        g.Update(ref frame, in xs) &&

        // Return the value
        @return(ref frame, in x) 

            ? @continue(ref frame)
            : empty(ref frame);
}
