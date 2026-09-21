using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static partial class IterOperators
{
    extension<A, B>(Iter<A>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<A, B> operator *(in Iter<A> lhs, in Iter<B> rhs) =>
            new (in lhs, in rhs);        
    }
    
    extension<A, B, C>(IterProduct<A, B>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<A, B, C> operator *(in IterProduct<A, B> lhs, in Iter<C> rhs) =>
            new (lhs.ta, lhs.tb, in rhs);
    }

    extension<A, B, C, D>(IterProduct<A, B, C>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<A, B, C, D> operator *(in IterProduct<A, B, C> lhs, in Iter<D> rhs) =>
            new (lhs.ta, lhs.tb, lhs.tc, in rhs);
    }

    extension<A, B, C, D, E>(IterProduct<A, B, C, D>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<A, B, C, D, E> operator *(in IterProduct<A, B, C, D> lhs, in Iter<E> rhs) =>
            new (lhs.ta, lhs.tb, lhs.tc, lhs.td, in rhs);
    }

    extension<A, B, C, D, E, F>(IterProduct<A, B, C, D, E>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<A, B, C, D, E, F> operator *(in IterProduct<A, B, C, D, E> lhs, in Iter<F> rhs) =>
            new (lhs.ta, lhs.tb, lhs.tc, lhs.td, lhs.te, in rhs);
    }

    extension<A, B, C, D, E, F, G>(IterProduct<A, B, C, D, E, F>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<A, B, C, D, E, F, G> operator *(in IterProduct<A, B, C, D, E, F> lhs, in Iter<G> rhs) =>
            new (lhs.ta, lhs.tb, lhs.tc, lhs.td, lhs.te, lhs.tf, in rhs);
    }

    extension<A, B, C, D, E, F, G, H>(IterProduct<A, B, C, D, E, F, G>)
    {
        [MethodImpl(Optimisations.Default)]
        public static IterProduct<(A, B, C, D, E, F, G), H> operator *(in IterProduct<A, B, C, D, E, F, G> lhs, in Iter<H> rhs) =>
            lhs.ToIterator() * rhs;
    }
}