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
    public static unsafe int iterators2<A>(ref StackFrame frame) =>

        // Pop the iterator
        PullStruct.arg1<Iter<A>>(ref frame, out var ta) &&

        // Read the next value
        ta.TryGetValue(out var x, out var xs)

            // Push the updated iterator
            ? PullStruct.update1(ref frame, in xs) &&

              // Return the value
              @return(ref frame, in x)

                  ? @continue(ref frame)
                  : empty(ref frame)

            // Load the second iterator
            : PullStruct.arg2<Iter<A>>(ref frame, out var ys) &&

              // Set arg1 to the second iterator
              PullStruct.update1(ref frame, ys) &&

              // Set the iterator function to process a single iterator only
              setIteratorFunction(ref frame, &iterator<A>)

                // Run the single iterator operation
                ? iterator<A>(ref frame)
                : PullState.Void;

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe int iterators(ref StackFrame frame) =>
        
        getIteratorFunction(ref frame, out var f)
            ? f(ref frame)
            : PullState.Void;

    [MethodImpl(Optimisations.InliningOnly)]
    static unsafe bool getIteratorFunction(ref StackFrame frame, out IterOp f)
    {
        if (PullUnmanaged.arg3<nint>(ref frame, out var f1))
        {
            f = (IterOp)f1;
            return true;

        }
        else
        {
            f = default!;
            return false;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    static unsafe bool setIteratorFunction(ref StackFrame frame, in IterOp f)
    {
        ref var g = ref PullUnmanaged.arg3<nint>(ref frame);
        g = (nint)f;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe IterOp iteratorsOp<A>() =>
        &iterators2<A>;
}
