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
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> Order() =>
            Root.Arr.create(T.Sort(ta)).Forward();

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> Order(IComparer<A> comparer) =>
            Root.Arr.create(T.Sort(ta, comparer.Compare)).Forward();

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> Order(Comparison<A> comparer) =>
            Root.Arr.create(T.Sort(ta,comparer)).Forward();

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> OrderBy<Key>(Func<A, Key> key) =>
            Root.Arr.create(T.SortBy(ta, key, OrdDefault<Key>.Compare)).Forward();

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> OrderBy<Key>(Func<A, Key> key, IComparer<Key> comparer) =>
            Root.Arr.create(T.SortBy(ta, key, comparer.Compare)).Forward();

        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> OrderBy<Key>(Func<A, Key> key, Comparison<Key> comparer) =>
            Root.Arr.create(T.SortBy(ta, key, comparer)).Forward();
    }

    extension<T, OrdA, A>(K<T, A> ta)
        where OrdA : Ord<A>
        where T : Iterable<T>
    {
        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> Order() =>
            Root.Arr.create(T.Sort(ta, LE.OrdComparer<OrdA, A>.Default.Compare)).Forward();
    }

    extension<T, A, OrdKey, Key>(K<T, A> ta)
        where OrdKey : Ord<Key>
        where T : Iterable<T>
    {
        /// <summary>
        /// Yield items in ascending order 
        /// </summary>
        /// <returns>Iterator</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Root.Iterator<A> OrderBy(Func<A, Key> key) =>
            Root.Arr.create(T.SortBy(ta, key, LE.OrdComparer<OrdKey, Key>.Default.Compare)).Forward();
    }
}