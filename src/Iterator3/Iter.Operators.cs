using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static partial class IterOperators
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
        public static Iter<B> operator |(in Iter<A> lhs, in IterMap<A, B> rhs) =>
            IterAction.map(rhs.f, in lhs);
    
        [MethodImpl(Optimisations.Default)]
        public static Iter<B> operator >>(in Iter<A> lhs, in IterBind<A, B> rhs) =>
            IterAction.bind(in lhs, rhs.f);
    }
}