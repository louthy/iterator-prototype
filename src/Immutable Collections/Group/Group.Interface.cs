using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

/// <summary>
/// A group of items.
/// </summary>
/// <remarks>
/// The result of `GroupBy` and `GroupJoin` operations.
/// </remarks>
/// <typeparam name="A">Item value type</typeparam>
public interface Group<A> : LT.K<Group, A>
{
    /// <summary>
    /// Items in the group
    /// </summary>
    Iter<A> Items { get; }
}