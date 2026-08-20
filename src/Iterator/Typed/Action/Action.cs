using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A> : IteratorAction<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref object obj, ref IteratorAction self,ref Space128 space, out A head)
    {
        ref var ta  = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var ts  = ref Unsafe.As<Space128, IS>(ref space);
        ref var act = ref Unsafe.As<IteratorAction, IteratorAction<A>>(ref self);
        return TryGetValue(ref ta, ref act, ref ts, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref K<T, A> ta, ref IteratorAction<A> self, ref IS space, out A head);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    new IteratorAction<T, IS, A> Cons(A value) =>
        new ConsAction<T, IS, A>(value, this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<A> IteratorAction<A>.Cons(A value) =>
        Cons(value);
}
