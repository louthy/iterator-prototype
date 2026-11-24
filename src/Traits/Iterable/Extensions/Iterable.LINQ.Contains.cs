using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Returns true if the `value` is contained in the iterable sequence
        /// </summary>
        /// <remarks>
        /// Stops processing the sequence upon discovery of a matching value.
        /// </remarks>
        /// <param name="value">Value to find</param>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>True if the `value` is contained in the iterable sequence</returns>
        public bool Contains(in A value) =>
            T.Contains(value, ta);

        /// <summary>
        /// Returns true if the `value` is contained in the iterable sequence
        /// </summary>
        /// <remarks>
        /// Stops processing the sequence upon discovery of a matching value.
        /// </remarks>
        /// <param name="value">Value to find</param>
        /// <param name="eq">Equality comparer, for bespoke equality testing</param>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>True if the `value` is contained in the iterable sequence</returns>
        public bool Contains(in A value, IEqualityComparer<A> eq) =>
            T.Contains(value, eq, ta);

        /// <summary>
        /// Returns true if the `value` is contained in the iterable sequence
        /// </summary>
        /// <remarks>
        /// Stops processing the sequence upon discovery of a matching value.
        /// </remarks>
        /// <param name="value">Value to find</param>
        /// <param name="eq">Equality comparer function, for bespoke equality testing</param>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>True if the `value` is contained in the iterable sequence</returns>
        public bool Contains(in A value, Func<A, A, bool> eq) =>
            T.Contains(value, eq, ta);
    }
    
    extension<EqA, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where EqA : LT.Eq<A>
    {

        /// <summary>
        /// Returns true if the `value` is contained in the iterable sequence
        /// </summary>
        /// <remarks>
        /// Stops processing the sequence upon discovery of a matching value.
        /// </remarks>
        /// <param name="value">Value to find</param>
        /// <typeparam name="EqA">Equality trait</typeparam>
        /// <typeparam name="A">Element type</typeparam>
        /// <returns>True if the `value` is contained in the iterable sequence</returns>
        public bool Contains(in A value) =>
            T.Contains<EqA, A>(value, ta);
    }
}