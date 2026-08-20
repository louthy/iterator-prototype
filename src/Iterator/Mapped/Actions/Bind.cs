using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
sealed class BindAction<A, B>(IteratorAction<A> action, Func<A, Iterator<B>> f) : IteratorAction<A, B>
{
    static readonly Stack<BindStack<A, B>> stack = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out B head)
    {
        var     actionTyped   = action;
        ref var actionUntyped = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref actionTyped);
        
        while (action.TryGetValue(ref ta, ref actionUntyped, ref space, out var x))
        {
            var ib = f(x);
            if (ib.TryGetValue(out head, out var tail))
            {
                //self = new BindStack<A, B>(ta, this, space, tail.fields.action);
                self = Acquire(ta, this, space, tail.fields.action);
                tail.Prime(ref ta, ref space);
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
        if (stack.TryPop(out var element))
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
        stack.Push(element);
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
    bool IteratorAction<B>.TryGetValue(ref object tb, ref IteratorAction self, ref Space128 space, out B head)
    {
        if(bindAction.TryGetValue(ref tb, ref self, ref space, out head))
        {
            return true;
        }
        else
        {
            tb = savedTA;
            self = savedBind;
            space = savedSpace;

            released = true;
            BindAction<A, B>.Release(this);
            return savedBind.TryGetValue(ref tb, ref self, ref space, out head);
        }
    }

    ~BindStack()
    {
        if(!released) BindAction<A, B>.Release(this);
    }
}
