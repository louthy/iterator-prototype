using System.Collections;
using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

[SkipLocalsInit]
public struct IteratorEnumerator<TA, IS, A>(in Iterator<TA, IS, A> iterator) : IEnumerator<A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    readonly Iterator<TA, IS, A> original = iterator;
    Iterator<TA, IS, A> iterator = iterator;
    A? current;

    public bool MoveNext() =>
        iterator.TryGetValue(out current, out iterator);

    public void Reset() =>
        iterator = original;

    public A Current => 
        current!;

    object? IEnumerator.Current => 
        Current;

    public void Dispose()
    { }
}
