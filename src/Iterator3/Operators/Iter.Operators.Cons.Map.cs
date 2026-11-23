using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A, B>(IterCons<A>)
    {
        public static Iter<B> operator | (in IterCons<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B>(IterCons2<A>)
    {
        public static Iter<B> operator | (in IterCons2<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B>(IterCons3<A>)
    {
        public static Iter<B> operator | (in IterCons3<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B>(IterCons4<A>)
    {
        public static Iter<B> operator | (in IterCons4<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }
}