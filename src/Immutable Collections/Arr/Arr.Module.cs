using LanguageExt.Traits;
using IteratorPrototype.DSL;
using IteratorPrototype.Traits;
using static LanguageExt.Prelude;
using System.Diagnostics.Contracts;

namespace IteratorPrototype;

public partial class Arr
{
    /// <summary>
    /// Create an empty collection
    /// </summary>
    [Pure]
    public static Arr<A> empty<A>() =>
        Arr<A>.Empty;

    /// <summary>
    /// Create a singleton collection
    /// </summary>
    /// <param name="value">Single value</param>
    /// <returns>Collection with a single item in it</returns>
    [Pure]
    public static Arr<A> singleton<A>(A value) =>
        new ArrSingleton<A>(value);

    /// <summary>
    /// Create a collection from an initial set of items
    /// </summary>
    /// <param name="items">Items</param>
    /// <returns>A new collection</returns>
    [Pure]
    public static Arr<A> create<A>(params ReadOnlySpan<A> items) =>
        items.Length switch
        {
            0 => Arr<A>.Empty,
            1 => new ArrSingleton<A>(items[0]),
            _ => new ArrMany<A>([.. items], 0, items.Length)
        };

    /// <summary>
    /// Create a collection from an initial set of items
    /// </summary>
    /// <param name="items">Items</param>
    /// <param name="start">Start index</param>
    /// <param name="count">Number of items in the collection</param>
    /// <returns>A new collection</returns>
    [Pure]
    internal static Arr<A> createInternal<A>(A[] items, int start, int count) =>
        items.Length switch
        {
            0                             => Arr<A>.Empty,
            1                             => new ArrSingleton<A>(items[0]),
            var l when start + count <= l => new ArrMany<A>(items, start, count),
            _                             => throw new ArgumentOutOfRangeException()
        };

    /// <summary>
    /// Create a range of integers
    /// </summary>
    /// <param name="range">Range specification</param>
    /// <returns>A new collection</returns>
    [Pure]
    public static Arr<int> create(Range range)
    {
        var (start, count) = range.GetOffsetAndLength(int.MaxValue);
        switch (count)
        {
            case 0:
                return Arr<int>.Empty;
            
            case 1:
                return singleton(start);
            
            default:
                var xs = new int[count];
                var ix = 0;
                var ct = count;
                for (var x = start; ct > 0; ct--)
                {
                    xs[ix++] = x;
                }
                return new ArrMany<int>(xs, 0, count);
        }
    }

    /// <summary>
    /// Create a collection from an initial set of items
    /// </summary>
    /// <param name="items">Items</param>
    /// <returns>A new collection</returns>
    [Pure]
    public static Arr<A> create<T, A>(K<T, A> items) 
        where T : Iterable<T> =>
        [.. items.AsSpan()];    

    /// <summary>
    /// Create a collection from an initial set of items
    /// </summary>
    /// <param name="items">Items</param>
    /// <returns>A new collection</returns>
    [Pure]
    public static Arr<A> create<A>(IEnumerable<A> items) =>
        create([..items]);    

    /// <summary>
    /// Add an item to the array
    /// </summary>
    /// <param name="array">Array</param>
    /// <param name="value">Item to add</param>
    /// <returns>A new Lst T</returns>
    [Pure]
    public static Arr<A> add<A>(Arr<A> array, A value) =>
        array.Add(value);

    /// <summary>
    /// Add a range of items to the array
    /// </summary>
    /// <param name="array">Array</param>
    /// <param name="value">Items to add</param>
    /// <returns>A new Lst T</returns>
    [Pure]
    public static Arr<A> addRange<A>(Arr<A> array, IEnumerable<A> value) =>
        array.AddRange(value);

    /// <summary>
    /// Remove an item at a specified index in the array
    /// </summary>
    /// <param name="array">Array</param>
    /// <param name="index">Index of item to remove</param>
    /// <returns>A new Lst T</returns>
    [Pure]
    public static Arr<A> removeAt<A>(Arr<A> array, int index) =>
        array.RemoveAt(index);

    /// <summary>
    /// Monadic join
    /// </summary>
    [Pure]
    public static Arr<A> flatten<A>(Arr<Arr<A>> ma) =>
        ma.Bind(identity);
}
