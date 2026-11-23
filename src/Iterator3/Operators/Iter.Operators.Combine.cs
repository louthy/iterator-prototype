using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A>(Iter<A>)
    {
        public static IterCombine<A> operator +(in Iter<A> lhs, in Iter<A> rhs) =>
            new(lhs, rhs);
        
        public static IterCombine2<A> operator +(in IterCombine<A> lhs, in Iter<A> rhs) =>
            new(lhs.ta, lhs.tb, rhs);        
        
        public static IterCombine3<A> operator +(in IterCombine2<A> lhs, in Iter<A> rhs) =>
            new(lhs.ta, lhs.tb, lhs.tc, rhs);        
        
        public static IterCombine4<A> operator +(in IterCombine3<A> lhs, in Iter<A> rhs) =>
            new(lhs.ta, lhs.tb, lhs.tc, lhs.td, rhs);        
        
        public static IterCombine4<A> operator +(in IterCombine4<A> lhs, in Iter<A> rhs) =>
            lhs with { te = lhs.te + rhs };        
    }
}