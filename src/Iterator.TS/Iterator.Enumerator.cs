using IteratorTest.Traits;

namespace IteratorTest;

public struct IteratorEnumerator<T, TS, A>(in Iterator<T, TS, A> iterator)
    where T : IterableK<T, TS>
    where TS : struct
{
    readonly Iterator<T, TS, A> original = iterator;
    Iterator<T, TS, A> iterator = iterator;
    A? current;

    public bool MoveNext() =>
        iterator.TryGetValue(out current, out iterator);

    public void Reset() =>
        iterator = original;

    public A Current => 
        current!;
}
