using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public struct IteratorEnumerator<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    readonly Iterator<T, IS, A> reset;
    Iterator<T, IS, A> iter;
    A current;
    
    [MethodImpl(Optimisations.Default)]
    public IteratorEnumerator(in Iterator<T, IS, A> iter)
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