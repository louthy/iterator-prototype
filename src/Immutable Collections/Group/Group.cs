using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

/// <summary>
/// A group of items, labelled by a value.
/// </summary>
/// <remarks>
/// The result of `GroupBy` and `GroupJoin` operations.
/// </remarks>
/// <param name="Label">Value that discriminates this group.</param>
/// <param name="Items">Items in the group</param>
/// <typeparam name="A">Group discriminator type</typeparam>
/// <typeparam name="B">Item value type</typeparam>
public readonly record struct Group<A, B>(in A Label, in Iter<B> Items) : Group<B>
{
    /// <summary>
    /// Items in the group
    /// </summary>
    Iter<B> Group<B>.Items { get; } = Items;

    public static implicit operator Iter<B>(in Group<A, B> g) =>
        g.Items;

    public static implicit operator A(in Group<A, B> g) =>
        g.Label;    
}
