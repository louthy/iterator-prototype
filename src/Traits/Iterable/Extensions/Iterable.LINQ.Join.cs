using IteratorPrototype.Iterator3;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<Key, T, A, B, C>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
    {
        /// <summary>
        /// Correlates the elements of two sequences based on matching keys.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">Takes the element from the first sequence and the element from the second sequence
        /// and maps to a new result-value.</param>
        /// <param name="comparer">Bespoke comparer for comparing the keys</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="C">The type of the result elements.</typeparam>
        /// <returns>A sequence of values that represent the inner-join of the two input sequences.</returns>
        public Iter<C> Join(
            LT.K<T, B> tb,
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            Func<A, B, C> resultSelector,
            IEqualityComparer<Key> comparer) =>
            T.Join(keySelectorA, keySelectorB, resultSelector, comparer, ta, tb);
    
        /// <summary>
        /// Correlates the elements of two sequences based on matching keys.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">Takes the element from the first sequence and the element from the second sequence
        /// and maps to a new result-value.</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="C">The type of the result elements.</typeparam>
        /// <returns>A sequence of values that represent the inner-join of the two input sequences.</returns>
        public Iter<C> Join(
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            Func<A, B, C> resultSelector,
            LT.K<T, B> tb) =>
            T.Join(keySelectorA, keySelectorB, resultSelector, ta, tb);
    }

    extension<EqKey, Key, T, A, B, C>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
        where EqKey : LT.Eq<Key> 
    {
        /// <summary>
        /// Correlates the elements of two sequences based on matching keys.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">Takes the element from the first sequence and the element from the second sequence
        /// and maps to a new result-value.</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="EqKey">Key equality trait.</typeparam>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="C">The type of the result elements.</typeparam>
        /// <returns>A sequence of values that represent the inner-join of the two input sequences.</returns>
        public Iter<C> Join(
            LT.K<T, B> tb,
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            Func<A, B, C> resultSelector) =>
            T.Join<EqKey, Key, A, B, C>(keySelectorA, keySelectorB, resultSelector, ta, tb);
    }
}