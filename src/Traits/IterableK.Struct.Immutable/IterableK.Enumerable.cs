using System.Collections;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public class IteratorEnumerable<T, IS, A>(K<T, A> ta) : IEnumerable<A>
    where T : IterableK<T, IS>
    where IS : struct
{
    public IterableKEnumerator<T, IS, A> GetEnumerator() =>
        new (ta);
    
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
