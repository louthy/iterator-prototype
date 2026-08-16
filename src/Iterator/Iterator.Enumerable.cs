using System.Collections;
using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorEnumerable<A>(in Iterator<A> ta) : IEnumerable<A>
{
    readonly Iterator<A> ta = ta;
    
    public IteratorEnumerator<A> GetEnumerator() =>
        new (in ta);
    
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
