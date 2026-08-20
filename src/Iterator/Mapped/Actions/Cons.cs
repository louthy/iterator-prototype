using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<T, IS, A, B>(B Head, IteratorAction<T, IS, A, B> Then) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out B head)
    {
        head = Head;
        self = Then;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref K<T, A> ta, ref IteratorAction<B> self, ref IS space, out B head)
    {
        head = Head;
        self = Then;
        return true;
    }
}

