using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class IdAction<T, IS, A> : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public static readonly IteratorAction<T, IS, A> Default = new IdAction<T, IS, A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref object obj, ref IteratorAction self, ref Space128 space, out A head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return T.StepImmutable(in ta, in ts, out head, out ts);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorAction<B> Map<B>(Func<A, B> f) =>
        new MapAction<T, IS, A, B>(this, f);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A>.TryGetValue(ref K<T, A> ta, ref IteratorAction<T, IS, A> self, ref IS ts, out A head) =>
        T.Next(in ta, ref ts, out head);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<T, IS, A> IteratorAction<T, IS, A>.Cons(A value) =>
        new ConsAction<T, IS, A>(value, this);
}
