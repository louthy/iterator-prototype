using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

/// <summary>
/// A group of values, partitioned by a key.
/// </summary>
/// <remarks>
/// The result of a `GroupBy` operation.
/// </remarks>
/// <param name="Key">Key</param>
/// <param name="Values">Values</param>
/// <typeparam name="K">Key type</typeparam>
/// <typeparam name="V">Value type</typeparam>
public readonly record struct Group<K, V>(K Key, Iter<V> Values) : Group<V>
{
    /// <summary>
    /// Values implementation from the base-interface
    /// </summary>
    Iter<V> Group<V>.Values { get; } = Values;
}
