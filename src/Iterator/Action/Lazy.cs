using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyIteratorAction<A>(Func<Iterator<A>> xs) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorStack> stack, out A head)
    {
        if (xs().TryGetValue(out head, out var tail))
        {
            tail.Prime(ref stack);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }
}