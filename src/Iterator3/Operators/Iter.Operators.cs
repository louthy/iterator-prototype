using IteratorPrototype.Iterator3;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A>(bool)
    {
        public static A operator | (bool _, in A rhs) =>
            rhs;
    }
    
    extension<A>(K<Iter, A>)
    {
        public static Iter<A> operator +(in K<Iter, A> unary) =>
            (Iter<A>)unary;
    }

    extension<A>(Iter<A>)
    {
        public static Iter<A> operator | (in Iter<A> lhs, IterPure _) =>
            lhs;
        
        public static Iter<A> operator | (in Iter<A> lhs, IterTake rhs) =>
            lhs.Take(rhs.amount);
        
        public static Iter<A> operator | (in Iter<A> lhs, IterSkip rhs) =>
            lhs.Skip(rhs.amount);
    }
    
    extension<A, B>(Iter<A>)
    {
        public static Iter<B> operator |(in Iter<A> lhs, in IterMap<A, B> rhs) =>
            IterAction.map(rhs.f, in lhs);
    
        public static Iter<B> operator >>(in Iter<A> lhs, in IterBind<A, B> rhs) =>
            IterAction.bind(in lhs, rhs.f);
    }
}