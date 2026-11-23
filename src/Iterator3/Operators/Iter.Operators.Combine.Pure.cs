using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A>(IterCombine<A>)
    {
        public static Iter<A> operator | (in IterCombine<A> lhs, in IterPure rhs) =>
            lhs;
    }

    extension<A>(IterCombine2<A>)
    {
        public static Iter<A> operator | (in IterCombine2<A> lhs, in IterPure rhs) =>
            lhs;
    }

    extension<A>(IterCombine3<A>)
    {
        public static Iter<A> operator | (in IterCombine3<A> lhs, in IterPure rhs) =>
            lhs;
    }

    extension<A>(IterCombine4<A>)
    {
        public static Iter<A> operator | (in IterCombine4<A> lhs, in IterPure rhs) =>
            lhs;
    }
}