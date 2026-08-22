using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static partial class IteratorOperators
{
    extension<A>(A self)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<A> operator +(in A x, in Iterator<A> xs) =>
            xs.Cons(x);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<A> operator +(in A x, Func<Iterator<A>> xs)
        {
            var fields = new IteratorFields<A>(x!, new LazyIteratorAction<A>(xs), default);
            return new Iterator<A>(in fields);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator<A> Cons(Func<Iterator<A>> xs)
        {
            var fields = new IteratorFields<A>(null!, new LazyConsIteratorAction<A>(self, xs), default);
            return new Iterator<A>(in fields);
        }
    }
}
