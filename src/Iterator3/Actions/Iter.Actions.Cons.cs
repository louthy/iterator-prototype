#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly record struct IterCons<A>(in A a, in Iter<A> tr)
{
    public static implicit operator Iter<A>(IterCons<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.cons(a, tr);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        Iter.cons(f(a), tr.Map(f));
}

[SkipLocalsInit]
public readonly record struct IterCons2<A>(in A a, in A b, in Iter<A> tr)
{
    public static implicit operator Iter<A>(IterCons2<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.cons(a, b, tr);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        Iter.cons(f(a), f(b), tr.Map(f));
}

[SkipLocalsInit]
public readonly record struct IterCons3<A>(in A a, in A b, in A c, in Iter<A> tr)
{
    public static implicit operator Iter<A>(IterCons3<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.cons(a, b, c, tr);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        Iter.cons(f(a), f(b), f(c), tr.Map(f));
}

[SkipLocalsInit]
public readonly record struct IterCons4<A>(in A a, in A b, in A c, in A d, in Iter<A> tr)
{
    public static implicit operator Iter<A>(IterCons4<A> p) =>
        p.ToIterator();

    public Iter<A> ToIterator() =>
        Iter.cons(a, b, c, d, tr);

    internal Iter<B> Map<B>(Func<A, B> f) =>
        Iter.cons(f(a), f(b), f(c), f(d), tr.Map(f));
}
