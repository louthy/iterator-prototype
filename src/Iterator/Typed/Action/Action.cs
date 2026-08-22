using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A> : IteratorAction<A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref MiniStack<IteratorFields> stack, out A head) =>
        TryGetValue(ref stack.Cast<IteratorFields, IteratorFields<T, IS, A>>(), out head);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref MiniStack<IteratorFields<T, IS, A>> stack, out A head);
}
