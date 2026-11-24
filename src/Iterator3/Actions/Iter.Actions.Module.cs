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
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> take<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.take(in frame, amount)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> takeUntil<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.takeUntil(in frame, f)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> takeWhile<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.takeWhile(in frame, f)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> skip<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.skip(in frame, amount)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> skipUntil<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.skipUntil(in frame, f)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> skipWhile<A>(Func<A, bool> f, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.skipWhile(in frame, f)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> map<A, B>(Func<A, B> f, in Iter<A> iterator)
    {
        var frame = iterator.Next<A, B>(out var tb);
        return Push.map(in frame, f)
                   ? tb
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> map<A, B, C>(Func<A, B, C> f, in Iter<(A, B)> iterator)
    {
        var frame = iterator.Next<(A, B), C>(out var tc);
        return Push.bimap(in frame, f)
                   ? tc
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<D> map<A, B, C, D>(Func<A, B, C, D> f, in Iter<(A, B, C)> iterator)
    {
        var frame = iterator.Next<(A, B, C), D>(out var td);
        return Push.trimap(in frame, f)
                   ? td
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<E> map<A, B, C, D, E>(Func<A, B, C, D, E> f, in Iter<(A, B, C, D)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D), E>(out var te);
        return Push.quadmap(in frame, f)
                   ? te
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<F> map<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f, in Iter<(A, B, C, D, E)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E), F>(out var tf);
        return Push.pentamap(in frame, f)
                   ? tf
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<G> map<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f, in Iter<(A, B, C, D, E, F)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E, F), G>(out var tg);
        return Push.sextamap(in frame, f)
                   ? tg
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<H> map<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f, in Iter<(A, B, C, D, E, F, G)> iterator)
    {
        var frame = iterator.Next<(A, B, C, D, E, F, G), H>(out var th);
        return Push.septamap(in frame, f)
                   ? th
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> bind<A, B>(in Iter<A> ta, Func<A, Iter<B>> f)
    {
        var frame = Iter<B>.Default(out var tb);
        return Push.bind(in frame, ta, f)
                   ? tb
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<C> selectMany<A, B, C>(in Iter<A> ta, Func<A, Iter<B>> f, Func<A, B, C> project) =>
        // TODO: Optimise this to run as a single operation
        bind(in ta, a => map(b => project(a, b), f(a)));
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> choose<A, B>(Func<A, LE.Option<B>> f, in Iter<A> iterator)
    {
        var frame = iterator.Next<A, B>(out var tb);
        return Push.choose(in frame, f)
                   ? tb
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> filter<A>(Func<A, bool> f, in Iter<A> iterator)
    {
        var frame = iterator.Next(out var tb);
        return Push.filter(in frame, f)
                   ? tb
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> distinct<EqA, A>(ReadOnlySpan<A> seen, in Iter<A> iterator)
        where EqA : LT.Eq<A>
    {
        var frame = iterator.Next(out var tb);
        return Push.distinct<EqA, A>(in frame, seen)
                   ? tb
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> distinct<EqKey, Key, A>(Func<A, Key> f, ReadOnlySpan<Key> seen, in Iter<A> iterator)
        where EqKey : LT.Eq<Key>
    {
        var frame = iterator.Next(out var tb);
        return Push.distinct<EqKey, Key, A>(in frame, f, seen)
                   ? tb
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    internal static Iter<A> scope<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Insert.scope(in frame)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    internal static Iter<A> pure<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.pure(in frame)
                   ? ta1
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
}