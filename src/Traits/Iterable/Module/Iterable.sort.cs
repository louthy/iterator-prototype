using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class Iterable
{
    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sort<T, A>(K<T, A> ta) 
        where T : Iterable<T> =>
        T.Sort(ta);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sort<T, A>(IComparer<A> comparer, K<T, A> ta) 
        where T : Iterable<T> =>
        T.Sort(ta, comparer.Compare);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sort<T, A>(Comparison<A> comparer, K<T, A> ta) 
        where T : Iterable<T> =>
        T.Sort(ta,comparer);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sortBy<T, A, Key>(Func<A, Key> key, K<T, A> ta) 
        where T : Iterable<T> =>
        T.SortBy(ta, key, OrdDefault<Key>.Compare);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sortBy<T, A, Key>(Func<A, Key> key, IComparer<Key> comparer, K<T, A> ta) 
        where T : Iterable<T> =>
        T.SortBy(ta, key, comparer.Compare);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sortBy<T, A, Key>(Func<A, Key> key, Comparison<Key> comparer, K<T, A> ta) 
        where T : Iterable<T> =>
        T.SortBy(ta, key, comparer);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sort<T, OrdA, A>(K<T, A> ta) 
        where OrdA : Ord<A>
        where T : Iterable<T> =>
        T.Sort(ta, LE.OrdComparer<OrdA, A>.Default.Compare);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>An array of sorted elements.</returns>
    /// <remarks>The return-type is an array to indicate that: to sort a set of values, all values must be available.
    /// Using sort with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN sorted.
    /// And because the resulting array-type is also an `Iterable`, all of the Iterable operators can continue to work
    /// on the sorted-set.
    /// </remarks>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Arr<A> sortBy<T, A, OrdKey, Key>(Func<A, Key> key, K<T, A> ta) 
        where OrdKey : Ord<Key>
        where T : Iterable<T> =>
        T.SortBy(ta, key, LE.OrdComparer<OrdKey, Key>.Default.Compare);
}