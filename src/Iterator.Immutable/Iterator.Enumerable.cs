using System.Collections;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorEnumerable<T, IS, A>(in Iterator<T, IS, A> ta) : IEnumerable<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    readonly Iterator<T, IS, A> ta = ta;
    
    public IteratorEnumerator<T, IS, A> GetEnumerator() =>
        new (in ta);
    
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
