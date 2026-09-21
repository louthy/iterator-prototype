using System.Runtime.CompilerServices;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(A First, B Second)> product<A, B>(in Iter<A> ta, in Iter<B> tb) =>
        map((a, b) => (a, b), ta, tb);

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(A First, B Second, C Third)> product<A, B, C>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc) =>
        map((a, b, c) => (a, b, c), ta, tb, tc);

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(A First, B Second, C Third, D Fourth)> product<A, B, C, D>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td) =>
        map((a, b, c, d) => (a, b, c, d), ta, tb, tc, td);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth)> product<A, B, C, D, E>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te) =>
        map((a, b, c, d, e) => (a, b, c, d, e), ta, tb, tc, td, te);
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> product<A, B, C, D, E, F>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf) =>
        map((a, b, c, d, e, f) => (a, b, c, d, e, f), ta, tb, tc, td, te, tf);
            
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth, G Seventh)> product<A, B, C, D, E, F, G>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf, in Iter<G> tg) =>
        map((a, b, c, d, e, f, g) => (a, b, c, d, e, f, g), ta, tb, tc, td, te, tf, tg);
}
