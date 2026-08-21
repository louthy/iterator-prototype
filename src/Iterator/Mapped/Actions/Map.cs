using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class MapAction<A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorStack> stack, out B head)
    {
        ref var top   = ref stack.Peek();
        var     saved = top.action;
        if (action.TryGetValue(ref stack, out var h))
        {
            head = f(h);
            ref var ttop = ref stack.Peek();
            if (ReferenceEquals(ttop.action, saved)) return true;
            ref var a = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref ttop.action);
            ttop.action = new MapAction<A, B>(a, f);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<C> IteratorAction<B>.Map<C>(Func<B, C> g) =>
        new MapAction<A, C>(action, x => g(f(x)));
}

[SkipLocalsInit]
public sealed class MapAction<T, IS, A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorStack> stack, out B head)
    {
        ref var top   = ref stack.Peek();
        var     saved = top.action;
        if (action.TryGetValue(ref stack, out var h))
        {
            head = f(h);
            if (ReferenceEquals(top.action, saved)) return true;
            ref var a = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref top.action);
            top.action = new MapAction<T, IS, A, B>(a, f);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A, B>.TryGetValue(ref MiniStack<IteratorStack<T, IS, A, B>> stack, out B head)
    {
        ref var top   = ref stack.Peek();
        var     saved = top.action;
        ref var s1    = ref MiniStack<IteratorStack<T, IS, A, B>>.Cast<IteratorStack>(ref stack);
        
        if (action.TryGetValue(ref s1, out var h))
        {
            head = f(h);
            ref var ttop = ref s1.Peek();
            if (ReferenceEquals(ttop.action, saved)) return true;
            ref var a = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref ttop.action);
            ttop.action = new MapAction<T, IS, A, B>(a, f);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<C> IteratorAction<B>.Map<C>(Func<B, C> g) =>
        new MapAction<T, IS, A, C>(action, x => g(f(x)));
}
