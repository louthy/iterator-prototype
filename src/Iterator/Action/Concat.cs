using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConcatAction<A>(IteratorAction<A> first, Iterator<A> next) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out A head)
    {
        if (first.TryGetValue(ref ta, ref self, ref space, out head))
        {
            return true;
        }
        else
        {
            if (next.TryGetValue(out head, out var tail))
            {
                tail.Prime(ref ta, ref self, ref space);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
