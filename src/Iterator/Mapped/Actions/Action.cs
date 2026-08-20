using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<A, B> : IteratorAction<B>
{
    /*[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref IteratorStack stack, out B head)
    {
        ref var s1 = ref IteratorStack<A, B>.From(ref stack);
        return TryGetValue(ref s1, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref IteratorStack<A, B> stack, out B head);*/
}

public interface IteratorAction<T, IS, A, B> : IteratorAction<A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(ref IteratorStack stack, out B head)
    {
        ref var s1 = ref IteratorStack<T, IS, A, B>.From(ref stack);
        return TryGetValue(ref s1, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref IteratorStack<T, IS, A, B> stack, out B head);
}
