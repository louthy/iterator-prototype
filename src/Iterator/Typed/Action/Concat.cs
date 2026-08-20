using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConcatAction<T, IS, A>(IteratorAction<T, IS, A> first, Iterator<T, IS, A> next) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref K<T, A> ta, ref IteratorAction<A> self, ref IS space, out A head)
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
