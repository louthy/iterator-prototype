using LanguageExt.Traits;

namespace IteratorPrototype;

public static class ArrOperators
{
    extension<A>(K<Arr, A> ta)
    {
        /// <summary>
        /// Downcast operator
        /// </summary>
        public static Arr<A> operator +(K<Arr, A> ma) =>
            (Arr<A>)ma;
        
        /// <summary>
        /// Downcast operator
        /// </summary>
        public static Arr<A> operator >> (K<Arr, A> ma, LE.Lower lower) =>
            (Arr<A>)ma;
    }
}