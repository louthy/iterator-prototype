using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

/// <summary>
/// A group of values.
/// </summary>
/// <remarks>
/// The result of a `GroupBy` operation.
/// </remarks>
/// <typeparam name="V">Value type</typeparam>
public interface Group<V> : LT.K<Group, V>
{
    Iter<V> Values { get; }
}