#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly record struct IterProduct<A, B>(in Iter<A> ta, in Iter<B> tb)
{
    public static implicit operator Iter<(A First, B Second)>(IterProduct<A, B> p) =>
        p.ToIterator();

    public Iter<(A First, B Second)> ToIterator() =>
        Iter.product(ta, tb);

    internal Iter<C> Map<C>(Func<A, B, C> f) =>
        Iter.map(f, ta, tb);
}

[SkipLocalsInit]
public readonly record struct IterProduct<A, B, C>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc)
{
    public static implicit operator Iter<(A First, B Second, C Third)>(IterProduct<A, B, C> p) =>
        p.ToIterator();

    public Iter<(A First, B Second, C Third)> ToIterator() =>
        Iter.product(ta, tb, tc);

    internal Iter<D> Map<D>(Func<A, B, C, D> f) =>
        Iter.map(f, ta, tb, tc);
}

[SkipLocalsInit]
public readonly record struct IterProduct<A, B, C, D>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td)
{
    public static implicit operator Iter<(A First, B Second, C Third, D Fourth)>(IterProduct<A, B, C, D> p) =>
        p.ToIterator();

    public Iter<(A First, B Second, C Third, D Fourth)> ToIterator() =>
        Iter.product(ta, tb, tc, td);

    internal Iter<E> Map<E>(Func<A, B, C, D, E> f) =>
        Iter.map(f, ta, tb, tc, td);
}

[SkipLocalsInit]
public readonly record struct IterProduct<A, B, C, D, E>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te)
{
    public static implicit operator Iter<(A First, B Second, C Third, D Fourth, E Fifth)>(IterProduct<A, B, C, D, E> p) =>
        p.ToIterator();

    public Iter<(A First, B Second, C Third, D Fourth, E Fifth)> ToIterator() =>
        Iter.product(ta, tb, tc, td, te);

    internal Iter<F> Map<F>(Func<A, B, C, D, E, F> f) =>
        Iter.map(f, ta, tb, tc, td, te);
}

[SkipLocalsInit]
public readonly record struct IterProduct<A, B, C, D, E, F>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf)
{
    public static implicit operator Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)>(IterProduct<A, B, C, D, E, F> p) =>
        p.ToIterator();

    public Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> ToIterator() =>
        Iter.product(ta, tb, tc, td, te, tf);

    internal Iter<G> Map<G>(Func<A, B, C, D, E, F, G> f) =>
        Iter.map(f, ta, tb, tc, td, te, tf);
}

[SkipLocalsInit]
public readonly record struct IterProduct<A, B, C, D, E, F, G>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf, in Iter<G> tg)
{
    public static implicit operator Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth, G Seventh)>(IterProduct<A, B, C, D, E, F, G> p) =>
        p.ToIterator();

    public Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth, G Seventh)> ToIterator() =>
        Iter.product(ta, tb, tc, td, te, tf, tg);

    internal Iter<H> Map<H>(Func<A, B, C, D, E, F, G, H> f) =>
        Iter.map(f, ta, tb, tc, td, te, tf, tg);
}
