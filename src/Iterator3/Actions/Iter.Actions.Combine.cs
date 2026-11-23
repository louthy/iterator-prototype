#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly record struct IterCombine<A>(in Iter<A> ta, in Iter<A> tb)
{
    public static implicit operator Iter<A>(IterCombine<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.flatten(ta, tb);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, ToIterator());
}

[SkipLocalsInit]
public readonly record struct IterCombine2<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc)
{
    public static implicit operator Iter<A>(IterCombine2<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.flatten(ta, tb, tc);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, ToIterator());
}

[SkipLocalsInit]
public readonly record struct IterCombine3<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc, in Iter<A> td)
{
    public static implicit operator Iter<A>(IterCombine3<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.flatten(ta, tb, tc, td);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, ToIterator());
}

[SkipLocalsInit]
public readonly record struct IterCombine4<A>(in Iter<A> ta, in Iter<A> tb, in Iter<A> tc, in Iter<A> td, in Iter<A> te)
{
    public static implicit operator Iter<A>(IterCombine4<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.flatten(ta, tb, tc, td, te);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, ToIterator());
}
