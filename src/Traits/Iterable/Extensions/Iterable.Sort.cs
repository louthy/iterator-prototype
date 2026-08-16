using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;
using LanguageExt.Traits;

public static partial class IterableExtensions
{
    extension<T, A>(K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> Sort() =>
            T.Sort(ta);

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> Sort(IComparer<A> comparer) =>
            T.Sort(ta, comparer.Compare);

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> Sort(Comparison<A> comparer) =>
            T.Sort(ta,comparer);

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> SortBy<Key>(Func<A, Key> key) =>
            T.SortBy(ta, key, OrdDefault<Key>.Compare);

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> SortBy<Key>(Func<A, Key> key, IComparer<Key> comparer) =>
            T.SortBy(ta, key, comparer.Compare);

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> SortBy<Key>(Func<A, Key> key, Comparison<Key> comparer) =>
            T.SortBy(ta, key, comparer);
    }

    extension<T, OrdA, A>(K<T, A> ta)
        where OrdA : Ord<A>
        where T : Iterable<T>
    {
        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> Sort() =>
            T.Sort(ta, LE.OrdComparer<OrdA, A>.Default.Compare);
    }

    extension<T, A, OrdKey, Key>(K<T, A> ta)
        where OrdKey : Ord<Key>
        where T : Iterable<T>
    {
        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>ReadOnlySpan</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> SortBy(Func<A, Key> key) =>
            T.SortBy(ta, key, LE.OrdComparer<OrdKey, Key>.Default.Compare);
    }
}