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
    public static int takeUntil<A>(in StackFrame frame)
    {
        // Get the amount 
        ref var f = ref PullManaged.arg1<Func<A, bool>>(in frame);
        peek<A>(in frame, out var x);

        if (f(x))
        {
            pop<A>(in frame);
            return PullState.Void;
        }
        else
        {
            return PullState.Continue;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int takeWhile<A>(in StackFrame frame)
    {
        // Get the amount 
        ref var f = ref PullManaged.arg1<Func<A, bool>>(in frame);
        peek<A>(in frame, out var x);

        if (f(x))
        {
            return PullState.Continue;
        }
        else
        {
            pop<A>(in frame);
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
    public static int skipUntil<A>(in StackFrame frame)
    {
        // Get the predicate 
        ref var f = ref PullManaged.arg1<Func<A, bool>>(in frame);
        peek<A>(in frame, out var x);
        if (f(x))
        {
            f = _ => true;
            return PullState.Continue;
        }
        else
        {
            pop<A>(in frame);
            return PullState.Skip;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int skipWhile<A>(in StackFrame frame)
    {
        // Get the predicate 
        ref var f = ref PullManaged.arg1<Func<A, bool>>(in frame);
        peek<A>(in frame, out var x);
        if (f(x))
        {
            pop<A>(in frame);
            return PullState.Skip;
        }
        else
        {
            f = _ => false;
            return PullState.Continue;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int choose<A, B>(in StackFrame frame)
    {
        // Get the predicate 
        var f = PullManaged.arg1<Func<A, LE.Option<B>>>(in frame);
        peek<A>(in frame, out var x);
        var mb = f(x);
        if (mb.IsSome)
        {
            pop<A>(in frame);
            push(in frame, (B)mb);
            return PullState.Continue;
        }
        else
        {
            pop<A>(in frame);
            return PullState.Skip;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int filter<A>(in StackFrame frame)
    {
        // Get the predicate 
        var f = PullManaged.arg1<Func<A, bool>>(in frame);
        peek<A>(in frame, out var x);
        if (f(x))
        {
            return PullState.Continue;
        }
        else
        {
            pop<A>(in frame);
            return PullState.Skip;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int distinct<EqA, A>(in StackFrame frame)
        where EqA : LT.Eq<A>
    {
        ref var set = ref PullStruct.arg1<LE.HashSet<EqA, A>>(in frame);
        peek<A>(in frame, out var x);
        if (set.Contains(x))
        {
            pop<A>(in frame);
            return PullState.Skip;
        }
        else
        {
            set = set.Add(x);
            return PullState.Continue;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int distinct<EqKey, Key, A>(in StackFrame frame)
        where EqKey : LT.Eq<Key>
    {
        ref var set = ref PullStruct.arg1<LE.HashSet<EqKey, Key>>(in frame);
        var     f   = PullManaged.arg2<Func<A, Key>>(in frame);
        peek<A>(in frame, out var x);
        var k = f(x);
        if (set.Contains(k))
        {
            pop<A>(in frame);
            return PullState.Skip;
        }
        else
        {
            set = set.Add(k);
            return PullState.Continue;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int index<A>(in StackFrame frame)
    {
        ref var ix = ref PullStruct.arg1<int>(in frame);
        pop<A>(in frame, out var x);
        push(in frame, (Index: ix, Item: x));
        ix++;
        return PullState.Continue;
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