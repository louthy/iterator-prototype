using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyConsIteratorAction<T, IS, A>(A x, LazyIteratorAction<T, IS, A> xs) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref IteratorStack stack, out A head)
    {
        head = x;
        stack.action = xs;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref IteratorStack<T, IS, A> stack, out A head)
    {
        head = x;
        stack.action = xs;
        return true;
    }
}
