using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConcatAction<A>(IteratorAction<A> first, Iterator<A> next) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        if (first.TryGetValue(ref stack, out head))
        {
            return true;
        }
        else
        {
            if (next.TryGetValue(out head, out var tail))
            {
                tail.Prime(ref stack);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
