using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class BindAction<A, B>(IteratorAction<A> action, Func<A, Iterator<B>> f) : IteratorAction<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out B head)
    {
        var     actionTyped   = action;
        ref var actionUntyped = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref actionTyped);
        
        while (action.TryGetValue(ref ta, ref actionUntyped, ref space, out var x))
        {
            var ib = f(x);
            if (ib.TryGetValue(out head, out var tail))
            {
                self = new BindStack<A, B>(ta, this, space, tail.fields.action);
                tail.Prime(ref ta, ref space);
                return true;
            }
        }
        
        head = default!;
        return false;
    }
}

[SkipLocalsInit]
public sealed class BindStack<A, B>(object savedTA, BindAction<A, B> savedBind, Space128 savedSpace, IteratorAction<B> bindAction) : IteratorAction<A, B>
{
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
            return false;
        }
    }
}
