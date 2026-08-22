using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<T, IS, A, B>(B Head, IteratorAction<T, IS, A, B> Then) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out B head)
    {
        head = Head;
        stack.SetAction(Then);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields<T, IS, A, B>> stack, out B head)
    {
        head = Head;
        stack.SetAction(in Then);
        return true;
    }
}

