using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class PureAction<T, IS, A> : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorAction<T, IS, A> Default = new PureAction<T, IS, A>();

    [MethodImpl(Optimisations.Default)]
    bool IteratorAction<A>.TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        ref var top   = ref stack.Cast<IteratorFields, IteratorFields<T, IS, A>>().Peek();
        ref var space = ref Unsafe.AsRef(in top.space);
        return T.StepImmutable(in top.ta, in space, out head, out space);
    }

    [MethodImpl(Optimisations.Default)]
    bool IteratorAction<T, IS, A>.TryGetValue(ref MiniStack<IteratorFields<T, IS, A>> stack, out A head)
    {
        ref var top   = ref stack.Peek();
        ref var space = ref Unsafe.AsRef(in top.space);
        return T.StepImmutable(in top.ta, in space, out head, out space);
    }
}
