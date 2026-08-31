using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyIteratorAction<T, IS, A>(Func<Iterator<T, IS, A>> xs) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        var iter = xs();
        if (iter.TryGetValueInternal(ref stack, out head))
        {
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(ref MiniStack<IteratorFields<T, IS, A>> stack, out A head)
    {
        var iter = xs();
        if (iter.TryGetValueInternal(ref stack, out head))
        {
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }
}
