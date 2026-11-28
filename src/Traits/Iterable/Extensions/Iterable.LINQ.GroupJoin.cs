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
        /// Correlates the elements of two sequences based on key equality and groups the results.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and
        /// a collection of matching elements from the second sequence.</param>
        /// <param name="comparer">Bespoke comparer for comparing the keys</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="C">The type of the result elements.</typeparam>
        /// <returns>An iterator that contains elements of type `C` that are obtained by performing a grouped join
        /// on two sequences.</returns>
        public Iter<C> GroupJoin(
            LT.K<T, B> tb,
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            Func<A, Iter<B>, C> resultSelector,
            IEqualityComparer<Key> comparer) =>
            T.GroupJoin(keySelectorA, keySelectorB, resultSelector, comparer, ta, tb);
        
        /// <summary>
        /// Correlates the elements of two sequences based on key equality and groups the results.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and
        /// a collection of matching elements from the second sequence.</param>
        /// <param name="ta">The first sequence to join.</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="C">The type of the result elements.</typeparam>
        /// <returns>An iterator that contains elements of type `C` that are obtained by performing a grouped join
        /// on two sequences.</returns>
        public Iter<C> GroupJoin(
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            Func<A, Iter<B>, C> resultSelector,
            LT.K<T, B> tb) =>
            T.GroupJoin(keySelectorA, keySelectorB, resultSelector, ta, tb);    
    }

    extension<EqKey, Key, T, A, B, C>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
        where EqKey : LT.Eq<Key> 
    {
        /// <summary>
        /// Correlates the elements of two sequences based on key equality and groups the results.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and
        /// a collection of matching elements from the second sequence.</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="EqKey">Key equality trait.</typeparam>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="C">The type of the result elements.</typeparam>
        /// <returns>An iterator that contains elements of type `C` that are obtained by performing a grouped join
        /// on two sequences.</returns>
        public Iter<C> GroupJoin(
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            Func<A, Iter<B>, C> resultSelector,
            LT.K<T, B> tb) =>
            T.GroupJoin<EqKey, Key, A, B, C>(keySelectorA, keySelectorB, resultSelector, ta, tb);
    }

    extension<Key, T, A, B>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
    {
        /// <summary>
        /// Correlates the elements of two sequences based on key equality and groups the results.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="comparer">Bespoke comparer for comparing the keys</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <returns>An iterator that contains grouping elements that are obtained by performing a grouped join
        /// on two sequences.</returns>
        public Iter<Group<A, B>> GroupJoin(
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            IEqualityComparer<Key> comparer,
            LT.K<T, B> tb) =>
            T.GroupJoin(keySelectorA, keySelectorB, comparer, ta, tb);
        
        /// <summary>
        /// Correlates the elements of two sequences based on key equality and groups the results.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <returns>An iterator that contains grouping elements that are obtained by performing a grouped join
        /// on two sequences.</returns>
        public Iter<Group<A, B>> GroupJoin(
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            LT.K<T, B> tb) =>
            T.GroupJoin(keySelectorA, keySelectorB, ta, tb);        
    }

    extension<EqKey, Key, T, A, B>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
        where EqKey : LT.Eq<Key>
    {
        /// <summary>
        /// Correlates the elements of two sequences based on key equality and groups the results.
        /// </summary>
        /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="tb">The sequence to join to the first sequence.</param>
        /// <typeparam name="EqKey">Key equality trait.</typeparam>
        /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
        /// <returns>An iterator that contains grouping elements that are obtained by performing a grouped join
        /// on two sequences.</returns>
        public Iter<Group<A, B>> GroupJoin(
            Func<A, Key> keySelectorA,
            Func<B, Key> keySelectorB,
            LT.K<T, B> tb) =>
            T.GroupJoin<EqKey, Key, A, B>(keySelectorA, keySelectorB, ta, tb);        
    }
}