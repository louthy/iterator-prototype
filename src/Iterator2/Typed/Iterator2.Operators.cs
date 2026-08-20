using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static partial class Iterator2Operators
{
    extension<T, IS, A>(A x)
        where T : Tr.IterableImmutable<T, IS>
        where IS : struct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<T, IS, A> Cons(Func<Iterator2<T, IS, A>> xs) =>
            new (null!, new LazyConsIteratorAction<T, IS, A>(x, new LazyIteratorAction<T, IS, A>(xs)), default);
    }

    extension<T, IS, A>(Iterator2<T, IS, A>)
        where T : Tr.IterableImmutable<T, IS>
        where IS : struct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator2<T, IS, A> operator +(A x, Iterator2<T, IS, A> xs) =>
            new (xs.fields.ta, (xs.fields.action ?? IdAction<T, IS, A>.Default).Cons(x), xs.fields.space);
    }
    
    extension<T, IS, A>(Func<Iterator2<T, IS, A>> self)
        where T : Tr.IterableImmutable<T, IS>
        where IS : struct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator2<T, IS, A> operator +(A x, Func<Iterator2<T, IS, A>> xs) =>
            new (null!, new LazyConsIteratorAction<T, IS, A>(x, new LazyIteratorAction<T, IS, A>(xs)), default);
        
    }
}
