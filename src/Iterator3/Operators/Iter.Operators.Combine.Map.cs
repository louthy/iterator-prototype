using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A, B>(IterCombine<A>)
    {
        public static Iter<B> operator | (in IterCombine<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B>(IterCombine2<A>)
    {
        public static Iter<B> operator | (in IterCombine2<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B>(IterCombine3<A>)
    {
        public static Iter<B> operator | (in IterCombine3<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }

    extension<A, B>(IterCombine4<A>)
    {
        public static Iter<B> operator | (in IterCombine4<A> lhs, in IterMap<A, B> rhs) =>
            lhs.Map(rhs.f);
    }
}