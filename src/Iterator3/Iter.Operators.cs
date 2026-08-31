using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static class IterOperators
{
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
        public static Iter<(A First, B Second, C Third)> operator |(Iter<(A, B)> lhs, Iter<C> rhs) =>
            throw new NotImplementedException();
    }
    
    extension<A, B, C>(Iter<(A, B)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<C> operator |(Iter<(A, B)> lhs, IterBimap<A, B, C> rhs) =>
            IterAction.bimap(rhs.f, in lhs);
    }
        
    extension<A, B, C, D>(Iter<(A, B, C)>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<D> operator |(Iter<(A, B, C)> lhs, IterTrimap<A, B, C, D> rhs) =>
            IterAction.trimap(rhs.f, in lhs);
    }
}