using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<A, B> : IteratorAction<B>;

public interface IteratorAction<T, IS, A, B> : IteratorAction<A, B>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(Optimisations.Default)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorFields> stack, out B head)
    {
        ref var s1 = ref stack.Cast<IteratorFields, IteratorFields<T, IS, A, B>>();
        return TryGetValue(ref s1, out head);
    }
    
    [MethodImpl(Optimisations.Default)]
    bool TryGetValue(ref MiniStack<IteratorFields<T, IS, A, B>> stack, out B head);
}
