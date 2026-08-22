using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static partial class IteratorOperators
{
    extension<T, IS, A>(A x)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator<T, IS, A> Cons(Func<Iterator<T, IS, A>> xs) =>
            new (null!, new LazyConsIteratorAction<T, IS, A>(x, new LazyIteratorAction<T, IS, A>(xs)), default);
    }

    extension<T, IS, A>(Iterator<T, IS, A>)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<T, IS, A> operator +(in A x, in Iterator<T, IS, A> xs) =>
            xs.Cons(in x);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<T, IS, A> operator +(in Iterator<T, IS, A> xs, in Iterator<T, IS, A> ys) =>
            xs.Concat(ys);
    }
    
    extension<T, IS, A>(Func<Iterator<T, IS, A>> self)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iterator<T, IS, A> operator +(A x, Func<Iterator<T, IS, A>> xs) =>
            new (null!, new LazyConsIteratorAction<T, IS, A>(x, new LazyIteratorAction<T, IS, A>(xs)), default);
        
    }
}
