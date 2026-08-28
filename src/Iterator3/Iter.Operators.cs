using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static class IterOperators
{
    extension<A, B>(Iter<A>)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iter<(A First, B Second)> operator |(Iter<A> lhs, Iter<B> rhs) =>
            Iter.product(in lhs, in rhs);
    
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iter<B> operator |(Iter<A> lhs, IterMap<A, B> rhs) =>
            IterAction.map(rhs.f, in lhs);
    }
    
    extension<A, B, C>(Iter<(A, B)>)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iter<(A First, B Second, C Third)> operator |(Iter<(A, B)> lhs, Iter<C> rhs) =>
            Iter.product(in lhs, in rhs);
    }
    
    extension<A, B>(Iter<B>)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iter<(A First, B Second)> operator |(Iter<B> lhs, IterPair<A, B> rhs) =>
            IterAction.pair<A, B>(in lhs);
    }
    
    extension<A, B, C>(Iter<(A, B)>)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iter<C> operator |(Iter<(A, B)> lhs, IterBimap<A, B, C> rhs) =>
            IterAction.bimap(rhs.f, in lhs);
    }
}