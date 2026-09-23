using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int bind<A, B>(in StackFrame frame)
    {
        ref var ta = ref PullStruct.arg1<Iter<A>>(in frame);
        ref var tb = ref PullStruct.arg2<Iter<B>>(in frame);
        ref var f  = ref PullManaged.arg3<Func<A, Iter<B>>>(in frame);
        
        while (true)
        {
            if (tb.TryGetValue(out var b))
            {
                return @return(in frame, b)
                           ? PullState.Continue
                           : PullState.Void;
            }

            if (ta.TryGetValue(out var a))
            {
                tb = f(a);
                continue;
            }

            return PullState.Void;
        }
    }    
    
    [MethodImpl(Optimisations.Default)]
    public static int flatten<A>(in StackFrame frame) =>

        iterator<A>(in frame) switch
        {
            PullState.Void =>

                // Pop the iterators
                PullStruct.arg2<Iter<Iter<A>>>(in frame, out var tta) &&

                // Read the next value
                tta.TryGetValue(out var ta) &&

                // Push the updated iterator
                PullStruct.update1(in frame, in ta) &&

                // Push the updated iterators
                PullStruct.update2(in frame, in tta)

                    // Run the iterator
                    ? iterator<A>(in frame)

                    // Done
                    : empty(in frame),

            var result =>
                result
        };
}