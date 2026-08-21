using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class PureAction<T, IS, A> : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public static readonly IteratorAction<T, IS, A> Default = new PureAction<T, IS, A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref MiniStack<IteratorStack> stack, out A head)
    {
        var top = MiniStack<IteratorStack>.Cast<IteratorStack<T, IS, A>>(ref stack).Peek();
        return T.StepImmutable(in top.ta, in top.space, out head, out top.space);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<T, IS, A>.TryGetValue(ref MiniStack<IteratorStack<T, IS, A>> stack, out A head)
    {
        var top = stack.Peek();
        return T.Next(in top.ta, ref top.space, out head);
    }
}
