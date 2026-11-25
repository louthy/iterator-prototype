using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Produces the set difference of two sequences.
        /// </summary>
        /// <remarks>
        /// The set difference of two sets is defined as the members of the first set that don't appear in the second set.
        /// This method returns those elements in `ta` that don't appear in `tb`. It doesn't return those elements in `tb
        /// that don't appear in `ta`. Only unique elements are returned.
        /// </remarks>
        /// <param name="tb">Second sequence</param>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>An array that contains the set difference of the elements of two sequences.</returns>
        public Arr<A> Except(LT.K<T, A> tb) =>
            T.Except(ta, tb);

        /// <summary>
        /// Produces the set difference of two sequences.
        /// </summary>
        /// <remarks>
        /// The set difference of two sets is defined as the members of the first set that don't appear in the second set.
        /// This method returns those elements in `ta` that don't appear in `tb`. It doesn't return those elements in `tb
        /// that don't appear in `ta`. Only unique elements are returned.
        /// </remarks>
        /// <param name="tb">Second sequence</param>
        /// <param name="comparer">Equality comparer</param>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>An array that contains the set difference of the elements of two sequences.</returns>
        public Arr<A> Except(LT.K<T, A> tb, IEqualityComparer<A> comparer) =>
            T.Except(ta, tb, comparer);
    }
    
    extension<EqA, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where EqA : LT.Eq<A>
    {
        /// <summary>
        /// Produces the set difference of two sequences.
        /// </summary>
        /// <remarks>
        /// The set difference of two sets is defined as the members of the first set that don't appear in the second set.
        /// This method returns those elements in `ta` that don't appear in `tb`. It doesn't return those elements in `tb
        /// that don't appear in `ta`. Only unique elements are returned.
        /// </remarks>
        /// <param name="tb">Second sequence</param>
        /// <typeparam name="EqA">Equality trait</typeparam>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>An array that contains the set difference of the elements of two sequences.</returns>
        public Arr<A> Except(LT.K<T, A> tb) =>
            T.Except<EqA, A>(ta, tb);
    }
}