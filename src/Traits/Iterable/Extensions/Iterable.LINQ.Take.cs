using IteratorPrototype.Iterator3;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a range.
        /// </summary>
        public Iter<A> Take(int count) =>
            T.Take(count, ta);

        /// <summary>
        /// Take items from the sequence whilst the predicate returns true.
        /// </summary>
        public Iter<A> TakeWhile(Func<A, bool> predicate) =>
            T.TakeWhile(predicate, ta);

        /// <summary>
        /// Take items from the sequence until the predicate returns true.
        /// </summary>
        public Iter<A> TakeUntil(Func<A, bool> predicate) =>
            T.TakeUntil(predicate, ta);
    }
}