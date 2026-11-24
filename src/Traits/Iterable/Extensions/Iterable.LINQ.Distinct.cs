using IteratorPrototype.Iterator3;
using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Return a range with duplicate elements removed.
        /// </summary>
        /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
        public Iter<A> Distinct(params ReadOnlySpan<A> seen) =>
            T.Distinct<EqDefault<A>, A>(seen, ta);
    }
    
    extension<Key, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Return a range with duplicate elements removed.
        /// </summary>
        /// <param name="key">Function that maps from the element-type to a key for distinct testing.</param>
        /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
        /// <returns>Projected lazy iterator</returns>
        public Iter<A> DistinctBy(Func<A, Key> key, params ReadOnlySpan<Key> seen) =>
            T.DistinctBy<EqDefault<Key>, Key, A>(key, seen, ta);        
    }

    extension<EqA, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where EqA : LT.Eq<A>
    {
        /// <summary>
        /// Return a range with duplicate elements removed.
        /// </summary>
        /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
        /// <returns>Projected lazy iterator</returns>
        public Iter<A> Distinct(params ReadOnlySpan<A> seen) =>
            T.Distinct<EqA, A>(seen, ta);
    }

    extension<EqKey, Key, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where EqKey : LT.Eq<Key>
    {
        /// <summary>
        /// Return a range with duplicate elements removed.
        /// </summary>
        /// <param name="key">Function that maps from the element-type to a key for distinct testing.</param>
        /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
        /// <returns>Projected lazy iterator</returns>
        public Iter<A> DistinctBy(Func<A, Key> key, params ReadOnlySpan<Key> seen) =>
            T.DistinctBy<EqKey, Key, A>(key, seen, ta);        
    }
}