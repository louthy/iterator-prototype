using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(K<T, A> ta)
        where T : Iterable<T>
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
        public Arr<A> OrderDesc() =>
            T.Sort(ta, (x, y) => 0 - OrdDefault<A>.Compare(x, y));

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
        public Arr<A> OrderDesc(IComparer<A> comparer) =>
            T.Sort(ta, (x, y) => 0 - comparer.Compare(x, y));

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
        public Arr<A> OrderDesc(Comparison<A> comparer) =>
            T.Sort(ta, (x, y) => 0 - comparer(x, y));

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
        public Arr<A> OrderByDesc<Key>(Func<A, Key> key) =>
            T.SortBy(ta, key, (x, y) => 0 - OrdDefault<Key>.Compare(x, y));

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
        public Arr<A> OrderByDesc<Key>(Func<A, Key> key, IComparer<Key> comparer) =>
            T.SortBy(ta, key, (x, y) => 0 - comparer.Compare(x, y));

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
        public Arr<A> OrderByDesc<Key>(Func<A, Key> key, Comparison<Key> comparer) =>
            T.SortBy(ta, key, (x, y) => 0 - comparer(x, y));
    }

    extension<T, OrdA, A>(K<T, A> ta)
        where OrdA : Ord<A>
        where T : Iterable<T>
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
        public Arr<A> OrderDesc() =>
            T.Sort(ta, (x, y) => 0 - LE.OrdComparer<OrdA, A>.Default.Compare(x, y));
    }

    extension<T, A, OrdKey, Key>(K<T, A> ta)
        where OrdKey : Ord<Key>
        where T : Iterable<T>
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
        public Arr<A> OrderByDesc(Func<A, Key> key) =>
            T.SortBy(ta, key, (x, y) => 0 - LE.OrdComparer<OrdKey, Key>.Default.Compare(x, y));
    }
}