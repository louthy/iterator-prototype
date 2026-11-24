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
        public A Average(LT.K<T, A> fa) =>
            T.Average(fa);
    }

    extension<T, A, B>(LT.K<T, A> ta)
        where T : Iterable<T>
        where B : INumber<B>
    {
        /// <summary>
        /// Finds the average of a sequence of numbers
        /// </summary>
        public B Average(LT.K<T, A> fa, Func<A, B> map) =>
            T.Average(fa, map);
    }
}