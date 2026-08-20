using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyIteratorAction<T, IS, A>(Func<Iterator2<T, IS, A>> xs) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out A head)
    {
        var iter = xs();
        if (iter.TryGetValueInternal(ref ta, ref self, ref space, out head))
        {
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref K<T, A> ta, ref IteratorAction<A> self, ref IS space, out A head)
    {
        var iter = xs();
        if (iter.TryGetValue(out head, out var tail))
        {
            ta = tail.fields.ta;
            self = tail.fields.action!;
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
