using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A> : IteratorAction<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref IteratorStack stack, out A head)
    {
        ref var s1 = ref IteratorStack<T, IS, A>.From(ref stack);
        return TryGetValue(ref s1, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref IteratorStack<T, IS, A> stack, out A head);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    new IteratorAction<T, IS, A> Cons(A value) =>
        new ConsAction<T, IS, A>(value, this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<A> IteratorAction<A>.Cons(A value) =>
        Cons(value);
}
