using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
sealed class BindAction<A, B>(IteratorAction<A> action, Func<A, Iterator<B>> f) : IteratorAction<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorStack> stack, out B head)
    {
        while (true)
        {
            if (stack.Top == 1)
            {
                while (action.TryGetValue(ref stack, out var x))
                {
                    var ib = f(x);
                    if (ib.TryGetValue(out head, out var tail))
                    {
                        ref var fs = ref Unsafe.AsRef(in tail.fields);
                        ref var tb = ref Unsafe.AsRef(in fs.ta);
                        ref var tact = ref Unsafe.As<IteratorAction<B>, IteratorAction>(ref Unsafe.AsRef(in fs.action));
                        ref var ts = ref Unsafe.AsRef(in fs.space);
                        var     entry = new IteratorStack(ref tb, ref tact, ref ts);
                        stack.Push(in entry);
                        return true;
                    }
                }

                head = default!;
                return false;
            }
            else
            {
                ref var top = ref stack.Peek();
                ref var b   = ref Unsafe.As<IteratorAction, IteratorAction<B>>(ref Unsafe.AsRef(in top.action));
                if (b.TryGetValue(ref stack, out head))
                {
                    return true;
                }
                else
                {
                    stack.Pop();
                }
            }
        }
    }
}


/*
[SkipLocalsInit]
sealed class BindAction<A, B>(IteratorAction<A> action, Func<A, Iterator<B>> f) : IteratorAction<A, B>
{
    static readonly Stack<BindStack<A, B>> bindStack = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorStack> stack, out B head)
    {
        while (action.TryGetValue(ref stack, out var x))
        {
            var ib = f(x);
            if (ib.TryGetValue(out head, out var tail))
            {
                ref var top = ref stack.Peek();
                top.action = Acquire(top.ta, this, top.space, tail.fields.action);
                tail.Prime(ref top.ta, ref top.space);
                return true;
            }
        }

        head = default!;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static BindStack<A, B> Acquire(
        in object savedTA,
        in BindAction<A, B> savedBind,
        in Space128 savedSpace,
        in IteratorAction<B> bindAction)
    {
        if (bindStack.TryPop(out var element))
        {
            element.savedTA = savedTA;
            element.savedBind = savedBind;
            element.savedSpace = savedSpace;
            element.bindAction = bindAction;
            return element;
        }
        else
        {
            return new BindStack<A, B>(savedTA, savedBind, savedSpace, bindAction);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void Release(BindStack<A, B> element)
    {
        element.savedTA = null!;
        element.bindAction = null!;
        bindStack.Push(element);
    }
}

[SkipLocalsInit]
sealed class BindStack<A, B>(object savedTA, BindAction<A, B> savedBind, Space128 savedSpace, IteratorAction<B> bindAction) : IteratorAction<A, B>
{
    public object savedTA = savedTA;
    public BindAction<A, B> savedBind = savedBind;
    public Space128 savedSpace = savedSpace;
    public IteratorAction<B> bindAction = bindAction;
    bool released;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorStack> stack, out B head)
    {
        if(bindAction.TryGetValue(ref stack, out head))
        {
            return true;
        }
        else
        {
            ref var top = ref stack.Peek();
            top.ta = savedTA;
            top.action = savedBind;
            top.space = savedSpace;

            released = true;
            BindAction<A, B>.Release(this);
            return savedBind.TryGetValue(ref stack, out head);
        }
    }

    ~BindStack()
    {
        if(!released) BindAction<A, B>.Release(this);
    }
}
*/
