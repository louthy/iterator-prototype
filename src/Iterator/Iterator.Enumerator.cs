namespace IteratorTest;

public struct IteratorEnumerator<A>(in Iterator<A> iterator)
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
}
