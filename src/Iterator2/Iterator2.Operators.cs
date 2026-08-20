using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static partial class Iterator2Operators
{
    extension<A>(A self)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator2<A> operator +(A x, Iterator2<A> xs) =>
            new (xs.fields.ta, xs.fields.action.Cons(x), xs.fields.space);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator2<A> operator +(A x, Func<Iterator2<A>> xs) =>
            new (x!, new LazyIteratorAction<A>(xs), default);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Cons(Func<Iterator2<A>> xs) =>
            new (self!, new LazyIteratorAction<A>(xs), default);
    }
}
