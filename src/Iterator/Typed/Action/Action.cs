using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A> : IteratorAction<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref MiniStack<IteratorStack> stack, out A head) =>
        TryGetValue(ref MiniStack<IteratorStack>.Cast<IteratorStack<T, IS, A>>(ref stack), out head);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref MiniStack<IteratorStack<T, IS, A>> stack, out A head);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    new IteratorAction<T, IS, A> Cons(A value) =>
        new ConsAction<T, IS, A>(value, this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<A> IteratorAction<A>.Cons(A value) =>
        Cons(value);
}
