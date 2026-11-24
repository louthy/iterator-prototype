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
        public Iter<A> Skip(int count) =>
            T.Skip(count, ta);

        /// <summary>
        /// Skip items at the start of the sequence whilst the predicate returns true.
        /// </summary>
        public Iter<A> SkipWhile(Func<A, bool> predicate) =>
            T.SkipWhile(predicate, ta);

        /// <summary>
        /// Skip items at the start of the sequence until the predicate returns true.
        /// </summary>
        public Iter<A> SkipUntil(Func<A, bool> predicate) =>
            T.SkipUntil(predicate, ta);
    }
}