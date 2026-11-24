using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<S, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Applies an accumulator function over a range 
        /// </summary>
        public S Aggregate(S state, Func<S, A, S> folder) =>
            T.Aggregate(state, folder, ta);
    }    
    
    extension<S, T, A, R>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Applies an accumulator function over a range and then maps the result
        /// </summary>
        public R Aggregate(S state, Func<S, A, S> folder, Func<S, R> map) =>
            T.Aggregate(state, folder, map, ta);

    }
}