namespace IteratorPrototype.Iterator3;

public partial class Iter
{
    public static Iter<A> combine<A>(in Iter<A> ta, in Iter<A> tb) =>
        flatten(ta, tb);
    
    public static Iter<A> combine<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc) =>
        flatten(ta, tb, tc);
    
    public static Iter<A> combine<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc, in Iter<A> td) =>
        flatten(ta, tb, tc, td);
    
    public static Iter<A> combine<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc, in Iter<A> td, in Iter<A> te) =>
        flatten(ta, tb, tc, td, te);
    
    public static Iter<A> combine<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc, in Iter<A> td, in Iter<A> te, in Iter<A> tf) =>
        flatten(ta, tb, tc, td, te, tf);
    
    public static Iter<A> combine<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc, in Iter<A> td, in Iter<A> te, in Iter<A> tf, in Iter<A> tg) =>
        flatten(ta, tb, tc, td, te, tf, tg);
}
