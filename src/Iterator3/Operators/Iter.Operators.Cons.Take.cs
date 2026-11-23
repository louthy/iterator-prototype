using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A>(IterCons<A>)
    {
        public static Iter<A> operator |(in IterCons<A> lhs, in IterTake rhs) =>
            lhs.ToIterator().Take(rhs.amount);
    }

    extension<A>(IterCons2<A>)
    {
        public static Iter<A> operator |(in IterCons2<A> lhs, in IterTake rhs) =>
            lhs.ToIterator().Take(rhs.amount);
    }

    extension<A>(IterCons3<A>)
    {
        public static Iter<A> operator |(in IterCons3<A> lhs, in IterTake rhs) =>
            lhs.ToIterator().Take(rhs.amount);
    }

    extension<A>(IterCons4<A>)
    {
        public static Iter<A> operator |(in IterCons4<A> lhs, in IterTake rhs) =>
            lhs.ToIterator().Take(rhs.amount);
    }
}