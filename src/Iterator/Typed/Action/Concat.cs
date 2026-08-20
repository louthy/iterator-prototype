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
    public bool TryGetValue(ref IteratorStack stack, out A head)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref IteratorStack<T, IS, A> stack, out A head)
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
