using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static partial class IteratorOperators
{
    extension<A>(A self)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<A> operator +(A x, Iterator<A> xs) =>
            new (xs.fields.ta, xs.fields.action.Cons(x), xs.fields.space);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<A> operator +(A x, Func<Iterator<A>> xs) =>
            new (x!, new LazyIteratorAction<A>(xs), default);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator<A> Cons(Func<Iterator<A>> xs) =>
            new (self!, new LazyIteratorAction<A>(xs), default);
    }
}
