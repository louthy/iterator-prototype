using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static class IterOperators
{
    extension<A>(bool)
    {
        [MethodImpl(Optimisations.Default)]
        public static A operator | (bool _, in A rhs) =>
            rhs;
    }
    
    extension<A>(Iter<A>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<A> operator +(in Iter<A> lhs, in Iter<A> rhs) =>
            Iter.flatten(lhs, rhs);
    }
    
    extension<A, B>(Iter<A>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second)> operator *(in Iter<A> lhs, in Iter<B> rhs) =>
            Iter.product(in lhs, in rhs);
    
        [MethodImpl(Optimisations.Default)]
        public static Iter<B> operator |(in Iter<A> lhs, in IterMap<A, B> rhs) =>
            IterAction.map(rhs.f, in lhs);
    
        [MethodImpl(Optimisations.Default)]
        public static Iter<B> operator >>(in Iter<A> lhs, in IterBind<A, B> rhs) =>
            IterAction.bind(in lhs, rhs.f);
    }
    
    extension<A, B, C>(Iter<(A, B)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<C> operator |(in Iter<(A, B)> lhs, in IterMap<A, B, C> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third)> operator *(in Iter<(A, B)> lhs, in Iter<C> rhs) =>
            Iter.product(in lhs, in rhs);
    }
        
    extension<A, B, C, D>(Iter<(A, B, C)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<D> operator |(in Iter<(A, B, C)> lhs, in IterMap<A, B, C, D> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth)> operator *(in Iter<(A, B, C)> lhs, in Iter<D> rhs) =>
            Iter.product(in lhs, in rhs);
    }
            
    extension<A, B, C, D, E>(Iter<(A, B, C, D)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<E> operator |(in Iter<(A, B, C, D)> lhs, in IterMap<A, B, C, D, E> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth)> operator *(in Iter<(A, B, C, D)> lhs, in Iter<E> rhs) =>
            Iter.product(in lhs, in rhs);
    }
                
    extension<A, B, C, D, E, F>(Iter<(A, B, C, D, E)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<F> operator |(in Iter<(A, B, C, D, E)> lhs, in IterMap<A, B, C, D, E, F> rhs) =>
            IterAction.map(rhs.f, in lhs);
        
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> operator *(in Iter<(A, B, C, D, E)> lhs, in Iter<F> rhs) =>
            Iter.product(in lhs, in rhs);
    }
}