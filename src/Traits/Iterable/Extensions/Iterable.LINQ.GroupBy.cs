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
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        public Iter<C> GroupBy(
            Func<A, Key> keySelector,
            Func<A, B> elementSelector,
            Func<Key, Iter<B>, LT.K<T, C>> resultSelector,
            IEqualityComparer<Key> comparer) =>
            T.GroupBy(keySelector, elementSelector, resultSelector, comparer, ta);

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        public Iter<C> GroupBy(
            Func<A, Key> keySelector,
            Func<A, B> elementSelector,
            Func<Key, Iter<B>, LT.K<T, C>> resultSelector) =>
            T.GroupBy(keySelector, elementSelector, resultSelector, ta);
    }

    extension<EqKey, Key, T, A, B, C>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
        where EqKey : LT.Eq<Key> 
    {
        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        public Iter<C> GroupBy(
            Func<A, Key> keySelector,
            Func<A, B> elementSelector,
            Func<Key, Iter<B>, LT.K<T, C>> resultSelector) =>
            T.GroupBy<EqKey, Key, A, B, C>(keySelector, elementSelector, resultSelector, ta);
    }

    extension<Key, T, A, B>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
    {
        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        public Iter<Group<Key, B>> GroupBy(
            Func<A, Key> keySelector,
            Func<A, B> elementSelector,
            IEqualityComparer<Key> comparer) =>
            T.GroupBy(keySelector, elementSelector, comparer, ta);

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        public Iter<Group<Key, B>> GroupBy(
            Func<A, Key> keySelector,
            Func<A, B> elementSelector) =>
            T.GroupBy(keySelector, elementSelector, ta);

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key.
        /// </summary>
        public Iter<B> GroupBy(
            Func<A, Key> keySelector,
            Func<Key, Iter<A>, LT.K<T, B>> resultSelector,
            IEqualityComparer<Key> comparer) =>
            T.GroupBy(keySelector, resultSelector, comparer, ta);
        
        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key.
        /// </summary>
        public Iter<B> GroupBy(
            Func<A, Key> keySelector,
            Func<Key, Iter<A>, LT.K<T, B>> resultSelector) =>
            T.GroupBy(keySelector, resultSelector, ta);
    }

    extension<EqKey, Key, T, A, B>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
        where EqKey : LT.Eq<Key>
    {
        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        public Iter<Group<Key, B>> GroupBy(
            Func<A, Key> keySelector,
            Func<A, B> elementSelector) =>
            T.GroupBy<EqKey, Key, A, B>(keySelector, elementSelector, ta);

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value
        /// from each group and its key.
        /// </summary>
        public Iter<B> GroupBy(
            Func<A, Key> keySelector,
            Func<Key, Iter<A>, LT.K<T, B>> resultSelector) =>
            T.GroupBy<EqKey, Key, A, B>(keySelector, resultSelector, ta);
    }

    extension<Key, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
    {
        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        public Iter<Group<Key, A>> GroupBy(
            Func<A, Key> keySelector,
            IEqualityComparer<Key> comparer) =>
            T.GroupBy(keySelector, comparer, ta);

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        public Iter<Group<Key, A>> GroupBy(
            Func<A, Key> keySelector) =>
            T.GroupBy(keySelector, ta);
    }

    extension<EqKey, Key, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where Key : notnull
        where EqKey : LT.Eq<Key>
    {
        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        public Iter<Group<Key, A>> GroupBy(
            Func<A, Key> keySelector) =>
            T.GroupBy<EqKey, Key, A>(keySelector, ta);
    }
}