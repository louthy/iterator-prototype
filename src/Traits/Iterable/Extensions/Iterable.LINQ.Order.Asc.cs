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
        public Arr<A> Order() =>
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
        public Arr<A> Order(IComparer<A> comparer) =>
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
        public Arr<A> Order(Comparison<A> comparer) =>
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
        public Arr<A> OrderBy<Key>(Func<A, Key> key) =>
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
        public Arr<A> OrderBy<Key>(Func<A, Key> key, IComparer<Key> comparer) =>
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
        public Arr<A> OrderBy<Key>(Func<A, Key> key, Comparison<Key> comparer) =>
            T.SortBy(ta, key, comparer);
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
        public Arr<A> Order() =>
            T.Sort(ta, LE.OrdComparer<OrdA, A>.Default.Compare);
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
        public Arr<A> OrderBy(Func<A, Key> key) =>
            T.SortBy(ta, key, LE.OrdComparer<OrdKey, Key>.Default.Compare);
    }
}