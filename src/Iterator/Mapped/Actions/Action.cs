using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<A, B> : IteratorAction<B>;

public interface IteratorAction<T, IS, A, B> : IteratorAction<A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorStack> stack, out B head)
    {
        ref var s1 = ref MiniStack<IteratorStack>.Cast<IteratorStack<T, IS, A, B>>(ref stack);
        return TryGetValue(ref s1, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref MiniStack<IteratorStack<T, IS, A, B>> stack, out B head);
}
