using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static class IterOperators
{
    extension<A>(bool)
    {
        [MethodImpl(Optimisations.Default)]
        public static A operator | (bool _, A rhs) =>
            rhs;
    }
    
    extension<A>(Iter<A>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<A> operator +(Iter<A> lhs, Iter<A> rhs) =>
            Iter.combine(in lhs, in rhs);
    }
    
    extension<A, B>(Iter<A>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second)> operator *(Iter<A> lhs, Iter<B> rhs) =>
            Iter.product(in lhs, in rhs);
    
        [MethodImpl(Optimisations.Default)]
        public static Iter<B> operator |(Iter<A> lhs, IterMap<A, B> rhs) =>
            IterAction.map(rhs.f, in lhs);
    }
    
    extension<A, B, C>(Iter<(A, B)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<C> operator |(Iter<(A, B)> lhs, IterMap<A, B, C> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third)> operator *(Iter<(A, B)> lhs, Iter<C> rhs) =>
            Iter.product(in lhs, in rhs);
    }
        
    extension<A, B, C, D>(Iter<(A, B, C)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<D> operator |(Iter<(A, B, C)> lhs, IterMap<A, B, C, D> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth)> operator *(Iter<(A, B, C)> lhs, Iter<D> rhs) =>
            Iter.product(in lhs, in rhs);
    }
            
    extension<A, B, C, D, E>(Iter<(A, B, C, D)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<E> operator |(Iter<(A, B, C, D)> lhs, IterMap<A, B, C, D, E> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth)> operator *(Iter<(A, B, C, D)> lhs, Iter<E> rhs) =>
            Iter.product(in lhs, in rhs);
    }
                
    extension<A, B, C, D, E, F>(Iter<(A, B, C, D, E)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<F> operator |(Iter<(A, B, C, D, E)> lhs, IterMap<A, B, C, D, E, F> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> operator *(Iter<(A, B, C, D, E)> lhs, Iter<F> rhs) =>
            Iter.product(in lhs, in rhs);
    }
}