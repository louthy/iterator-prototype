using System.Runtime.CompilerServices;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> map<A, B>(Func<A, B> f, in Iter<A> ta)
    {
        var frame = ta.Next<A, B>(out var tb);
        return Push.map(in frame, f)
                   ? tb
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> map<A, B, C>(Func<A, B, C> f, in Iter<A> ta, in Iter<B> tb)
    {
        var frame = ta.Next<A, C>(out var tc);
        return Push.yield<A>(in frame) &&
               Push.iterator(in frame, in tb) && 
               Push.bimap(in frame, f)
                   ? tc
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<D> map<A, B, C, D>(Func<A, B, C, D> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc)
    {
        var frame = ta.Next<A, D>(out var td);
        return Push.yield<A>(in frame)        &&
               Push.iterator(in frame, in tb) &&
               Push.yield<B>(in frame)        &&
               Push.iterator(in frame, in tc) && 
               Push.trimap(in frame, f) 
                   ? td
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<E> map<A, B, C, D, E>(Func<A, B, C, D, E> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td)
    {
        var frame = ta.Next<A, E>(out var te);
        return Push.yield<A>(in frame)        &&
               Push.iterator(in frame, in tb) &&
               Push.yield<B>(in frame)        &&
               Push.iterator(in frame, in tc) &&
               Push.yield<C>(in frame)        &&
               Push.iterator(in frame, in td) && 
               Push.quadmap(in frame, f)
                   ? te
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<F> map<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te)
    {
        var frame = ta.Next<A, F>(out var tf);
        return Push.yield<A>(in frame)        &&
               Push.iterator(in frame, in tb) &&
               Push.yield<B>(in frame)        &&
               Push.iterator(in frame, in tc) &&
               Push.yield<C>(in frame)        &&
               Push.iterator(in frame, in td) &&
               Push.yield<D>(in frame)        &&
               Push.iterator(in frame, in te) && 
               Push.pentamap(in frame, f) 
                   ? tf
                   : default;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<G> map<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf)
    {
        var frame = ta.Next<A, G>(out var tg);
        return Push.yield<A>(in frame)        &&
               Push.iterator(in frame, in tb) &&
               Push.yield<B>(in frame)        &&
               Push.iterator(in frame, in tc) &&
               Push.yield<C>(in frame)        &&
               Push.iterator(in frame, in td) &&
               Push.yield<D>(in frame)        &&
               Push.iterator(in frame, in te) &&
               Push.yield<E>(in frame)        &&
               Push.iterator(in frame, in tf) && 
               Push.sextamap(in frame, f)
                   ? tg
                   : default;
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<H> map<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f, in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf, in Iter<G> tg)
    {
        var frame = ta.Next<A, H>(out var th);
        return Push.yield<A>(in frame)        &&
               Push.iterator(in frame, in tb) &&
               Push.yield<B>(in frame)        &&
               Push.iterator(in frame, in tc) &&
               Push.yield<C>(in frame)        &&
               Push.iterator(in frame, in td) &&
               Push.yield<D>(in frame)        &&
               Push.iterator(in frame, in te) &&
               Push.yield<E>(in frame)        &&
               Push.iterator(in frame, in tf) &&
               Push.yield<F>(in frame)        &&
               Push.iterator(in frame, in tg) &&
               Push.septamap(in frame, f)
                   ? th
                   : default;
    }
}
