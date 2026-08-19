using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<T, IS, A, B> : IteratorAction<B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<B>.TryGetValue(in object obj, ref IteratorAction self, ref Space128 space, out B head)
    {
        ref readonly var ta  = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts  = ref Unsafe.As<Space128, IS>(ref space);
        ref var          act = ref Unsafe.As<IteratorAction, IteratorAction<T, IS, A, B>>(ref self);
        return TryGetValue(in ta, ref act, ref ts, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in K<T, A> ta, ref IteratorAction<T, IS, A, B> self, ref IS space, out B head);
}
