using System.Runtime.CompilerServices;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> map<A, B, C>(Func<A, B, C> f, in Iter<A> ta, in Iter<B> tb)
    {
        var frame = ta.Next<A, C>(out var tc);
        return Push.iterator(ref frame, in tb) && 
               Push.bimap(ref frame, f)
                   ? tc
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<D> map<A, B, C, D>(Func<A, B, C, D> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc)
    {
        var frame = ta.Next<A, D>(out var td);
        return Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) && 
               Push.trimap(ref frame, f) 
                   ? td
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<E> map<A, B, C, D, E>(Func<A, B, C, D, E> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td)
    {
        var frame = ta.Next<A, E>(out var te);
        return Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) && 
               Push.quadmap(ref frame, f)
                   ? te
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<F> map<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te)
    {
        var frame = ta.Next<A, F>(out var tf);
        return Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) &&
               Push.iterator(ref frame, in te) && 
               Push.pentamap(ref frame, f) 
                   ? tf
                   : default;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<G> map<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf)
    {
        var frame = ta.Next<A, G>(out var tg);
        return Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) &&
               Push.iterator(ref frame, in te) &&
               Push.iterator(ref frame, in tf) && 
               Push.sextamap(ref frame, f)
                   ? tg
                   : default;
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<H> map<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf, in Iter<G> tg)
    {
        var frame = ta.Next<A, H>(out var th);
        return Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) &&
               Push.iterator(ref frame, in te) &&
               Push.iterator(ref frame, in tf) &&
               Push.iterator(ref frame, in tg) &&
               Push.septamap(ref frame, f)
                   ? th
                   : default;
    }
}
