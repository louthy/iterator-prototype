using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public interface IteratorAction;

public interface IteratorAction<A> : IteratorAction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out A head);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<B> Map<B>(Func<A, B> f) =>
        new MapAction<A, B>(this, f);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<B> Bind<B>(Func<A, Iterator<B>> f) =>
        new BindAction<A, B>(this, f);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<A> Cons(A value) =>
        new ConsAction<A>(value, this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    IteratorAction<A> Concat(Iterator<A> rhs) =>
        new ConcatAction<A>(this, rhs);
}
