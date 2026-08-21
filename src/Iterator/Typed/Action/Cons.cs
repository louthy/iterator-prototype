using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<T, IS, A>(A Head, IteratorAction<T, IS, A> Then) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorStack> stack, out A head)
    {
        head = Head;
        stack.Peek().action = Then;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorStack<T, IS, A>> stack, out A head)
    {
        head = Head;
        stack.Peek().action = Then;
        return true;
    }
}
