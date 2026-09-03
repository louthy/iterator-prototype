#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    /// <summary>
    /// Await
    /// </summary>
    public static IterAwait await = default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static IterPure pure = default;
    
    /// <summary>
    /// Co-routine scope
    /// </summary>
    public static IterScope scope = default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static IterTake take(int amount) => 
        new (amount);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> from<A>(params ReadOnlySpan<A> ta)
    {
        var array = Arr.create(ta);
        var frame = Iter<A>.Default(out var iter);
        return Push.iterable<Arr, ArrState, A>(ref frame, array)
                   ? iter
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterable<T, IS, A>(ref frame, in ta)
                   ? iter
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> forever<A>(in A head)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.forever(ref frame, in head)
                    ? iter
                    : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> singleton<A>(in A head)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.singleton(ref frame, in head)
                   ? iter
                   : default;
    }
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> lift<A>(in Iter<A> ta)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterator(ref frame, in ta)
                   ? iter
                   : default;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<B> apply<A, B>(in Iter<Func<A, B>> tf, in Iter<A> ta)
    {
        // TODO: Consider how I can stack Ops, Vars, and set offsets for Globals, etc.
        
        throw new NotImplementedException();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> flatten<A>(params ReadOnlySpan<Iter<A>> ts)
    {
        var iters = from(ts);
        var frame = Iter<A>.Default(out var iter);
        return Push.flatten(ref frame, in iters)
                   ? iter
                   : default;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B> map<A, B>(Func<A, B> f) =>
        new (f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C> bimap<A, B, C>(Func<A, B, C> f) =>
        new(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D> trimap<A, B, C, D>(Func<A, B, C, D> f) =>
        new(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E> quadmap<A, B, C, D, E>(Func<A, B, C, D, E> f) =>
        new(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E, F> quadmap<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f) =>
        new(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E, F, G> pentamap<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f) =>
        new(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B> select<A, B>(Func<A, B> f) =>
        new (f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C> select<A, B, C>(Func<A, B, C> f) =>
        new(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D> select<A, B, C, D>(Func<A, B, C, D> f) =>
        new(f);    

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E> select<A, B, C, D, E>(Func<A, B, C, D, E> f) =>
        new(f);    

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E, F> select<A, B, C, D, E, F>(Func<A, B, C, D, E, F> f) =>
        new(f);    

    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E, F, G> select<A, B, C, D, E, F, G>(Func<A, B, C, D, E, F, G> f) =>
        new(f);    
}
