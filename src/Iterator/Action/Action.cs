using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public interface IteratorAction;

public interface IteratorAction<A> : IteratorAction
{
    [MethodImpl(Optimisations.Default)]
    bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head);
    
    [MethodImpl(Optimisations.Default)]
    IteratorAction<B> Map<B>(Func<A, B> f) =>
        new MapAction<A, B>(this, f);
    
    [MethodImpl(Optimisations.Default)]
    IteratorAction<B> Bind<B>(Func<A, Iterator<B>> f) =>
        new BindAction<A, B>(this, f);

    [MethodImpl(Optimisations.Default)]
    IteratorAction<A> Concat(in Iterator<A> rhs) =>
        new ConcatAction<A>(this, rhs);
}
