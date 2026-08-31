using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public struct IteratorEnumerator<A>
{
    readonly Iterator<A> reset;
    Iterator<A> iter;
    A current;
    
    [MethodImpl(Optimisations.Default)]
    public IteratorEnumerator(in Iterator<A> iter)
    {
        this.reset = iter;
        this.iter = iter;
        this.current = default!;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool MoveNext() =>
        iter.TryGetValue(out current, out iter);

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