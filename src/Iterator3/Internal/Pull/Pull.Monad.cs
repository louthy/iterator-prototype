using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int bind<A, B>(ref StackFrame frame)
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(ref frame);
        ref var tb = ref PullStruct.arg2<Iter<B>>(ref frame);
        ref var f  = ref PullManaged.arg3<Func<A, Iter<B>>>(ref frame);
        
        while (true)
        {
            if (tb.TryGetValue(out var b, out tb))
            {
                return @return(ref frame, b)
                           ? PullState.Continue
                           : PullState.Void;
            }

            if (ta.TryGetValue(out var a, out ta))
            {
                tb = f(a);
                continue;
            }

            return PullState.Void;
        }
    }    
    
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