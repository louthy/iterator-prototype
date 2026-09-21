using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static partial class IterOperators
{
    extension<A, B, C>(IterProduct<A, B>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<C> operator | (in IterProduct<A, B> lhs, in IterMap<A, B, C> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B, C, D>(IterProduct<A, B, C>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<D> operator | (in IterProduct<A, B, C> lhs, in IterMap<A, B, C, D> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B, C, D, E>(IterProduct<A, B, C, D>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<E> operator | (in IterProduct<A, B, C, D> lhs, in IterMap<A, B, C, D, E> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B, C, D, E, F>(IterProduct<A, B, C, D, E>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<F> operator | (in IterProduct<A, B, C, D, E> lhs, in IterMap<A, B, C, D, E, F> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B, C, D, E, F, G>(IterProduct<A, B, C, D, E, F>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<G> operator | (in IterProduct<A, B, C, D, E, F> lhs, in IterMap<A, B, C, D, E, F, G> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B, C, D, E, F, G, H>(IterProduct<A, B, C, D, E, F, G>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<H> operator | (in IterProduct<A, B, C, D, E, F, G> lhs, in IterMap<A, B, C, D, E, F, G, H> rhs) =>
            lhs.Map(rhs.f);
    }
}