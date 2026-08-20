using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class MapAction<A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out B head)
    {
        var     beforeTyped   = action;
        ref var beforeUntyped = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref beforeTyped);
        
        if (action.TryGetValue(ref ta, ref beforeUntyped, ref space, out var h))
        {
            head = f(h);
            if (!ReferenceEquals(action, beforeTyped))
            {
                self = new MapAction<A, B>(beforeTyped, f);
            }
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorAction<B> Cons(B value) =>
        new ConsAction<B>(value, this);
}

[SkipLocalsInit]
public sealed class MapAction<T, IS, A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out B head)
    {
        var     beforeTyped   = action;
        ref var beforeUntyped = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref beforeTyped);
        
        if (action.TryGetValue(ref ta, ref beforeUntyped, ref space, out var h))
        {
            head = f(h);
            if (!ReferenceEquals(action, beforeTyped))
            {
                self = new MapAction<T, IS, A, B>(beforeTyped, f);
            }
            return true;
        }
        else
        {
            head = default!;
            return false;
        }        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A, B>.TryGetValue(ref K<T, A> ta, ref IteratorAction<T, IS, A, B> self, ref IS ts, out B head)
    {
        ref var obj = ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in ta));
        ref var spc = ref Unsafe.As<IS, Space128>(ref ts);

        var     beforeTyped   = action;
        ref var beforeUntyped = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref beforeTyped);
        
        if (action.TryGetValue(ref obj, ref beforeUntyped, ref spc, out var h))
        {
            head = f(h);
            if (!ReferenceEquals(action, beforeTyped))
            {
                self = new MapAction<T, IS, A, B>(beforeTyped, f);
            }
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorAction<B> Cons(B value) =>
        new ConsAction<T, IS, A, B>(value, this);
}
