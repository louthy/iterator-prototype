using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A, B> : IteratorAction<B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(in object obj, ref Space128 space, out B head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return TryGetValue(in ta, ref ts, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in K<T, A> ta, ref IS space, out B head);
}

public sealed class MapAction1<T, IS, A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A, B>.TryGetValue(in K<T, A> ta, ref IS ts, out B head)
    {
        ref readonly var obj = ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in ta));
        ref var          spc = ref Unsafe.As<IS, Space128>(ref ts);

        if (action.TryGetValue(in obj, ref spc, out var h))
        {
            head = f(h);
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
        new MapAction1<T, IS, A, C>(action, x => g(f(x)));
}

public sealed class MapAction2<T, IS, A, B>(IteratorAction<T, IS, A> action, Func<A, B> f) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A, B>.TryGetValue(in K<T, A> ta, ref IS ts, out B head)
    {
        if (action.TryGetValue(in ta, ref ts, out var h))
        {
            head = f(h);
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
        new MapAction2<T, IS, A, C>(action, x => g(f(x)));
}
