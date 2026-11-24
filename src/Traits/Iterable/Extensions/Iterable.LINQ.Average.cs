using System.Numerics;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where A : INumber<A>
    {
        /// <summary>
        /// Finds the average of a sequence of numbers
        /// </summary>
        public A Average() =>
            T.Average(ta);
    }

    extension<T, A, B>(LT.K<T, A> ta)
        where T : Iterable<T>
        where B : INumber<B>
    {
        /// <summary>
        /// Finds the average of a sequence of numbers
        /// </summary>
        public B Average(Func<A, B> map) =>
            T.Average(map, ta);
    }
}