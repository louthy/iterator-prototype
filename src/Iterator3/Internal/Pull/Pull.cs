using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static int empty(in StackFrame frame) =>
        PullState.Void;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int pure(in StackFrame frame) =>
        PullState.Pure;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int pureV<A>(in StackFrame frame)
    {
        arg1<A>(in frame, out var x);
        frame.vars.Push(in x, false);
        return PullState.Pure;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int incYield(in StackFrame frame)
    {
        frame.tops.IncrementYields(); 
        return PullState.Continue;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static int @continue(in StackFrame frame) =>
        PullState.Continue;

    [MethodImpl(Optimisations.InliningOnly)]
    public static void coroutine1(in StackFrame frame) =>
        frame.StartScope();

    [MethodImpl(Optimisations.InliningOnly)]
    public static int coroutine(in StackFrame frame)
    {
        frame.StartScope();
        return PullState.Continue;
    }

    public static int cons<A>(in StackFrame frame)
    {
        ref var flag = ref PullUnmanaged.arg1<bool>(in frame);

        if (flag)
        {
            arg2<A>(in frame, out var value);
            flag = false;
            @return(in frame, value);
            return PullState.Continue;
        }
        else
        {
            ref var ta = ref PullStruct.arg3<Iter<A>>(in frame);
            if (ta.TryRef(out var x))
            {
                @return(in frame, x);
                return PullState.Continue;
            }
            else
            {
                return PullState.Void;
            }
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int tuple<A, B>(in StackFrame frame)
    {
        // Pop the second element
        pop<B>(in frame, out var b);

        // Pop the first element
        pop<A>(in frame, out var a);

        // Push the tuple
        @return(in frame, (a, b));

        return PullState.Continue;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int tuple<A, B, C>(in StackFrame frame)
    {
        // Pop the second element
        pop<C>(in frame, out var c);

        // Pop the second element
        pop<B>(in frame, out var b);

        // Pop the first element
        pop<A>(in frame, out var a);

        // Push the tuple
        @return(in frame, (a, b, c));

        return PullState.Continue;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int take(in StackFrame frame)
    {
        // Get the amount 
        ref var amount = ref PullUnmanaged.arg1<int>(in frame);
        if (amount > 0)
        {
            // Push the updated amount and continue
            amount--;
            return PullState.Continue;
        }
        else
        {
            // Taken all we can, so terminate the sequence.
            return PullState.Void;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int skip(in StackFrame frame)
    {
        // Get the amount 
        ref var amount = ref PullUnmanaged.arg1<int>(in frame);
        if (amount > 0)
        {
            // Push the updated amount and skip
            amount--;
            return PullState.Skip;
        }
        else
        {
            // Skipped what we needed to, so continue the sequence.
            return PullState.Continue;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int elements<A, B>(in StackFrame frame)
    {
        pop<(A, B)>(in frame, out var tuple);
        push(in frame, in tuple.Item2);
        push(in frame, in tuple.Item1);
        return PullState.Continue;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int elements<A, B, C>(in StackFrame frame)
    {
        pop<(A, B, C)>(in frame, out var tuple);
        push(in frame, in tuple.Item3);
        push(in frame, in tuple.Item2);
        push(in frame, in tuple.Item1);
        return PullState.Continue;
    }
}