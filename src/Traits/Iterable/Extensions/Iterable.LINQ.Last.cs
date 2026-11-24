using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Returns the last element of a range, or `None` value if the range contains no elements.
        /// </summary>
        public LE.Option<A> LastOrNone() =>
            T.Forward(ta).LastOrNone();

        /// <summary>
        /// Returns the last element of a range, or a default value if the range contains no elements.
        /// </summary>
        public A? LastOrDefault() =>
            T.Forward(ta).LastOrDefault();
    }
    
    extension<T, F, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where F : LT.Alternative<F>
    {
        /// <summary>
        /// Returns the last element of a range, or an alternative value if the range contains no elements.
        /// </summary>
        public LT.K<F, A> LastOrAlt() => 
            T.Forward(ta).LastOrAlt<F>();
    }    
}