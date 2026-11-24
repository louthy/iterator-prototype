using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Returns the first element of a range, or `None` value if the range contains no elements.
        /// </summary>
        public LE.Option<A> FirstOrNone() =>
            T.FirstOrNone(ta);

        /// <summary>
        /// Returns the first element of a range, or a default value if the range contains no elements.
        /// </summary>
        public A? FirstOrDefault() =>
            T.FirstOrDefault(ta);
    }
    
    extension<T, F, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where F : LT.Alternative<F>
    {
        /// <summary>
        /// Returns the first element of a range, or an alternative value if the range contains no elements.
        /// </summary>
        public LT.K<F, A> FirstOrAlt() => 
            T.FirstOrAlt<F, A>(ta);
    }    
}