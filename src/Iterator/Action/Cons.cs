using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<A>(A Head, IteratorAction<A> Then) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref IteratorStack stack, out A head)
    {
        head = Head;
        stack.action = Then;
        return true;
    }
}
