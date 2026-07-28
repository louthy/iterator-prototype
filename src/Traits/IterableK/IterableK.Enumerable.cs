using System.Collections;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public class IteratorEnumerable<T, TS, A>(K<T, A> ta) : IEnumerable<A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public IEnumerator<A> GetEnumerator() =>
        new Enum();

    IEnumerator IEnumerable.GetEnumerator() =>
        new Enum();

    // TODO
    class Enum : IEnumerator<A>
    {
        object? _current;
        A _current1;

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        A IEnumerator<A>.Current => _current1;

        object? IEnumerator.Current => _current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}