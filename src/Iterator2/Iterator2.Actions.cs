using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public interface IteratorAction;

public interface IteratorAction<A> : IteratorAction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in object ta, ref IteratorAction self, ref Space128 space, out A head);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<B> Map<B>(Func<A, B> f);
}
