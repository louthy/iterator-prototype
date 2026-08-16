using System.Collections;
using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public struct IteratorEnumerator<A>(in Iterator<A> iterator) : IEnumerator<A>
{
    readonly Iterator<A> original = iterator;
    Iterator<A> iterator = iterator;
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
