using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<OrdA, T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
        where OrdA : LT.Ord<A> 
    {
        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public LE.Option<A> Max() =>
            T.Max<OrdA, A>(ta);

        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public A Max(A initialMax) => 
            T.Max<OrdA, A>(ta).IfNone(initialMax);
    }

    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public LE.Option<A> Max() =>
            T.Max<OrdDefault<A>, A>(ta);
    
        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public A Max(A initialMax) => 
            T.Max<OrdDefault<A>, A>(ta).IfNone(initialMax);
    }
}