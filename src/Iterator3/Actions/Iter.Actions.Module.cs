#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

static class IterAction
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> lazy<A>(Func<Iter<A>> f)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.lazy(in frame, f)
                   ? iter
                   : throw new InvalidOperationException();
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> take<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.take(in frame, amount)
                   ? ta1
                   : take(amount, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> takeUntil<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.takeUntil(in frame, f)
                   ? ta1
                   : takeUntil(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> takeWhile<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.takeWhile(in frame, f)
                   ? ta1
                   : takeWhile(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> skip<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.skip(in frame, amount)
                   ? ta1
                   : skip(amount, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> skipUntil<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.skipUntil(in frame, f)
                   ? ta1
                   : skipUntil(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> skipWhile<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.skipWhile(in frame, f)
                   ? ta1
                   : skipWhile(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> map<A, B>(Func<A, B> f, in Iter<A> ta)
    {
        var frame = ta.Next<A, B>(out var tb);
        return Push.map(in frame, f)
                   ? tb
                   : map(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> map<A, B, C>(Func<A, B, C> f, in Iter<(A, B)> tab)
    {
        var frame = tab.Next<(A, B), C>(out var tc);
        return Push.bimap(in frame, f)
                   ? tc
                   : map(f, Iter.lift(in tab));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<D> map<A, B, C, D>(Func<A, B, C, D> f, in Iter<(A, B, C)> tabc)
    {
        var frame = tabc.Next<(A, B, C), D>(out var td);
        return Push.trimap(in frame, f)
                   ? td
                   : map(f, Iter.lift(in tabc));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<E> map<A, B, C, D, E>(Func<A, B, C, D, E> f, in Iter<(A, B, C, D)> tabcd)
    {
        var frame = tabcd.Next<(A, B, C, D), E>(out var te);
        return Push.quadmap(in frame, f)
                   ? te
                   : map(f, Iter.lift(in tabcd));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<F> map<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f, in Iter<(A, B, C, D, E)> tabcde)
    {
        var frame = tabcde.Next<(A, B, C, D, E), F>(out var tf);
        return Push.pentamap(in frame, f)
                   ? tf
                   : map(f, Iter.lift(in tabcde));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<G> map<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f, in Iter<(A, B, C, D, E, F)> tabcdef)
    {
        var frame = tabcdef.Next<(A, B, C, D, E, F), G>(out var tg);
        return Push.sextamap(in frame, f)
                   ? tg
                   : map(f, Iter.lift(in tabcdef));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<H> map<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f, in Iter<(A, B, C, D, E, F, G)> tabcdefg)
    {
        var frame = tabcdefg.Next<(A, B, C, D, E, F, G), H>(out var th);
        return Push.septamap(in frame, f)
                   ? th
                   : map(f, Iter.lift(in tabcdefg));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> bind<A, B>(in Iter<A> ta, Func<A, Iter<B>> f)
    {
        var frame = Iter<B>.Default(out var tb);
        return Push.bind(in frame, ta, f)
                   ? tb
                   : bind(Iter.lift(in ta), f);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> selectMany<A, B, C>(in Iter<A> ta, Func<A, Iter<B>> f, Func<A, B, C> project) =>
        // TODO: Optimise this to run as a single operation
        bind(in ta, a => map(b => project(a, b), f(a)));
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> choose<A, B>(Func<A, LE.Option<B>> f, in Iter<A> ta)
    {
        var frame = ta.Next<A, B>(out var tb);
        return Push.choose(in frame, f)
                   ? tb
                   : choose(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> filter<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var tb);
        return Push.filter(in frame, f)
                   ? tb
                   : filter(f, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> distinct<EqA, A>(ReadOnlySpan<A> seen, in Iter<A> ta)
        where EqA : LT.Eq<A>
    {
        var frame = ta.Next(out var tb);
        return Push.distinct<EqA, A>(in frame, seen)
                   ? tb
                   : distinct<EqA, A>(seen, Iter.lift(in ta));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> distinct<EqKey, Key, A>(Func<A, Key> f, ReadOnlySpan<Key> seen, in Iter<A> ta)
        where EqKey : LT.Eq<Key>
    {
        var frame = ta.Next(out var tb);
        return Push.distinct<EqKey, Key, A>(in frame, f, seen)
                   ? tb
                   : distinct<EqKey, Key, A>(f, seen, Iter.lift(in ta));
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> scope<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.scope(in frame)
                   ? ta1
                   : scope(Iter.lift(in ta));
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> pure<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.pure(in frame)
                   ? ta1
                   : pure(Iter.lift(in ta));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<(int Index, A Item)> index<A>(in Iter<A> ta)
    {
        var frame = Iter<(int Index, A Item)>.Default(out var ta1);
        return Push.index(in frame, in ta)
                   ? ta1
                   : index(Iter.lift(in ta));
    }
    
}