using IteratorPrototype.Iterator3;
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
        /// Find the minimum value in the structure
        /// </summary>
        public LE.Option<A> Min() =>
            T.Min<OrdA, A>(ta);

        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public A Min(A initialMin) => 
            T.Min<OrdA, A>(ta).IfNone(initialMin);
    }

    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public LE.Option<A> Min() =>
            T.Min<OrdDefault<A>, A>(ta);
    
        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public A Min(A initialMin) => 
            T.Min<OrdDefault<A>, A>(ta).IfNone(initialMin);
    }
}