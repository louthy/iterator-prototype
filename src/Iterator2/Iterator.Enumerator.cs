using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public struct IteratorEnumerator2<A>
{
    readonly Iterator2<A> reset;
    Iterator2<A> iter;
    A current;
    
    [MethodImpl(Optimisations.Default)]
    public IteratorEnumerator2(in Iterator2<A> iter)
    {
        this.iter = iter;
        reset = iter;
        current = default!;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool MoveNext() =>
        iter.MoveNext(out current);

    public A Current
    {
        [MethodImpl(Optimisations.Default)]
        get => current;
    }

    public void Reset()
    {
        iter = reset;
        current = default!;
    }
}