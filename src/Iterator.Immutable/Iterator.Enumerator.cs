using System.Collections;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public struct IteratorEnumerator<T, IS, A>(in Iterator<T, IS, A> iterator) : IEnumerator<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    readonly Iterator<T, IS, A> original = iterator;
    Iterator<T, IS, A> iterator = iterator;
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
