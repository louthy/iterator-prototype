using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class MapAction<A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref IteratorStack stack, out B head)
    {
        var saved = stack.action;
        if (action.TryGetValue(ref stack, out var h))
        {
            head = f(h);
            if (ReferenceEquals(stack.action, saved)) return true;
            ref var a = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref stack.action);
            stack.action = new MapAction<A, B>(a, f);
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
    bool IteratorAction<B>.TryGetValue(ref IteratorStack stack, out B head)
    {
        var saved = stack.action;
        if (action.TryGetValue(ref stack, out var h))
        {
            head = f(h);
            if (ReferenceEquals(stack.action, saved)) return true;
            ref var a = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref stack.action);
            stack.action = new MapAction<T, IS, A, B>(a, f);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A, B>.TryGetValue(ref IteratorStack<T, IS, A, B> stack, out B head)
    {
        var     saved = stack.action;
        ref var s1    = ref IteratorStack.From(ref stack);
        if (action.TryGetValue(ref s1, out var h))
        {
            head = f(h);
            if (ReferenceEquals(s1.action, saved)) return true;
            ref var a = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref s1.action);
            s1.action = new MapAction<T, IS, A, B>(a, f);
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
