using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public interface IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in object ta, ref Space128 space, out A head);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<B> Map<B>(Func<A, B> f);
}
