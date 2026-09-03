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
public readonly record struct IterBind<A, B>(Func<A, Iter<B>> f);

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
        return Push.bimap1(ref frame, f)
                   ? tc
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<D> map<A, B, C, D>(Func<A, B, C, D> f, in Iter<(A, B, C)> iterator)
    {
        var frame = iterator.Next<(A, B, C), D>(out var td);
        return Push.trimap1(ref frame, f)
                   ? td
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<E> map<A, B, C, D, E>(Func<A, B, C, D, E> f, in Iter<(A, B, C, D)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D), E>(out var te);
        return Push.quadmap1(ref frame, f)
                   ? te
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<F> map<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f, in Iter<(A, B, C, D, E)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E), F>(out var tf);
        return Push.pentamap1(ref frame, f)
                   ? tf
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