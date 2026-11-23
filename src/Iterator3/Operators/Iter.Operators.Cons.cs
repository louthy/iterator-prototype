using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A>(Iter<A>)
    {
        public static IterCons<A> operator +(in A a, in Iter<A> tr) =>
            new(a, tr);
    }
    
    extension<A>(IterCons<A>)
    {
        public static IterCons2<A> operator +(in A a, in IterCons<A> tr) =>
            new(a, tr.a, tr.tr);
    }    
        
    extension<A>(IterCons2<A>)
    {
        public static IterCons3<A> operator +(in A a, in IterCons2<A> tr) =>
            new(a, tr.a, tr.b, tr.tr);
    }
            
    extension<A>(IterCons3<A>)
    {
        public static IterCons4<A> operator +(in A a, in IterCons3<A> tr) =>
            new(a, tr.a, tr.b, tr.c, tr.tr);
    }
            
    extension<A>(IterCons4<A>)
    {
        public static IterCons<A> operator +(in A a, in IterCons4<A> tr) =>
            new(a, tr);
    }    
}