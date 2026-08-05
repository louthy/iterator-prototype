using System.Collections;
using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

[SkipLocalsInit]
public readonly struct IteratorEnumerable<TA, IS, A>(in Iterator<TA, IS, A> ta) : IEnumerable<A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    readonly Iterator<TA, IS, A> ta = ta;
    
    public IteratorEnumerator<TA, IS, A> GetEnumerator() =>
        new (in ta);
    
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
