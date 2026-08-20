using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyIteratorAction<A>(Func<Iterator<A>> xs) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out A head)
    {
        if (xs().TryGetValue(out head, out var tail))
        {
            ta = tail.fields.ta;
            self = tail.fields.action;
            space = tail.fields.space;
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }
}