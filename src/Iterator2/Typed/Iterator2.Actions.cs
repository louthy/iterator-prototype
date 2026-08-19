using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A> : IteratorAction<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(in object obj, ref Space128 space, out A head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return TryGetValue(in ta, ref ts, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in K<T, A> ta, ref IS space, out A head);
}

public sealed class IdAction<T, IS, A> : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public static readonly IteratorAction<T, IS, A> Default = new IdAction<T, IS, A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(in object obj, ref Space128 space, out A head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return T.StepImmutable(in ta, in ts, out head, out ts);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A>.TryGetValue(in K<T, A> ta, ref IS ts, out A head) =>
        T.Next(in ta, ref ts, out head);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<B> IteratorAction<A>.Map<B>(Func<A, B> f) =>
        new MapAction2<T, IS, A, B>(this, f);
}

public sealed class ConsAction<T, IS, A>(A Head) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A>.TryGetValue(in K<T, A> obj, ref IS space, out A head)
    {
        head = Head;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<B> IteratorAction<A>.Map<B>(Func<A, B> f) =>
        new MapAction2<T, IS, A, B>(this, f);
}
