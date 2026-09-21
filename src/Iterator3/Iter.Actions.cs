#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct IterAwait;

[SkipLocalsInit]
public readonly struct IterPure;

[SkipLocalsInit]
public readonly struct IterScope;

[SkipLocalsInit]
public readonly record struct IterTake(int amount);

[SkipLocalsInit]
public readonly record struct IterMap<A, B>(Func<A, B> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C>(Func<A, B, C> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D>(Func<A, B, C, D> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E>(Func<A, B, C, D, E> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f);

[SkipLocalsInit]
public readonly record struct IterMap<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f);

[SkipLocalsInit]
public readonly record struct IterBind<A, B>(Func<A, Iter<B>> f);

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

static class IterAction
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> take<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.take(ref frame, amount)
                   ? ta1
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> map<A, B>(Func<A, B> f, in Iter<A> iterator)
    {
        var frame = iterator.Next<A, B>(out var tb);
        return Push.map(ref frame, f)
                   ? tb
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> map<A, B, C>(Func<A, B, C> f, in Iter<(A, B)> iterator)
    {
        var frame = iterator.Next<(A, B), C>(out var tc);
        return Push.bimap(ref frame, f)
                   ? tc
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<D> map<A, B, C, D>(Func<A, B, C, D> f, in Iter<(A, B, C)> iterator)
    {
        var frame = iterator.Next<(A, B, C), D>(out var td);
        return Push.trimap(ref frame, f)
                   ? td
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<E> map<A, B, C, D, E>(Func<A, B, C, D, E> f, in Iter<(A, B, C, D)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D), E>(out var te);
        return Push.quadmap(ref frame, f)
                   ? te
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<F> map<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f, in Iter<(A, B, C, D, E)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E), F>(out var tf);
        return Push.pentamap(ref frame, f)
                   ? tf
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<G> map<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f, in Iter<(A, B, C, D, E, F)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E, F), G>(out var tg);
        return Push.sextamap(ref frame, f)
                   ? tg
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<H> map<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f, in Iter<(A, B, C, D, E, F, G)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E, F, G), H>(out var th);
        return Push.septamap(ref frame, f)
                   ? th
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> bind<A, B>(in Iter<A> ta, in Func<A, Iter<B>> f)
    {
        var frame = Iter<B>.Default(out var tb);
        return Push.bind(ref frame, ta, f)
                   ? tb
                   : default;
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    internal static Iter<A> scope<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.scope(ref frame)
                   ? ta1
                   : default;
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    internal static Iter<A> pure<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.pure(ref frame)
                   ? ta1
                   : default;
    }
}