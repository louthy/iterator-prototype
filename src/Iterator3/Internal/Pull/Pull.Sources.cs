using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
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
    public static int flatten<A>(ref StackFrame frame) =>

        iterator<A>(ref frame) switch
        {
            PullState.Void =>

                // Pop the iterators
                arg2<Iter<Iter<A>>>(ref frame, out var tta) &&

                // Read the next value
                tta.TryGetValue(out var ta, out var tta1) &&

                // Push the updated iterator
                update1(ref frame, in ta) &&

                // Push the updated iterators
                update2(ref frame, in tta1)

                    // Run the iterator
                    ? iterator<A>(ref frame)

                    : empty(ref frame),

            var result =>
                result

        };
}
