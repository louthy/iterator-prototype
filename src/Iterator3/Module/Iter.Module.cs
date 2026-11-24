#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public partial class Iter
{
    /// <summary>
    /// Empty iterator
    /// </summary>
    public static readonly IterEmpty empty = 
        default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static readonly IterPure pure = 
        default;

    /// <summary>
    /// Create a new co-routine scope 
    /// </summary>
    public static readonly IterScope scope = 
        default;

    /// <summary>
    /// Only yield unique elements 
    /// </summary>
    public static readonly IterDistinct distinct = 
        default;
    
    /// <summary>
    /// Take a number of elements
    /// </summary>
    /// <param name="amount">Number of elements to take</param>
    public static IterTake take(int amount) => 
        new (amount);
    
    /// <summary>
    /// Skip a number of elements
    /// </summary>
    /// <param name="amount">Number of elements to skip</param>
    public static IterSkip skip(int amount) => 
        new (amount);

    /// <summary>
    /// Lift a function into an iterator, such that it is evaluated on-demand, rather than now.
    /// </summary>
    public static Iter<A> lazy<A>(Func<Iter<A>> f) =>
        IterAction.lazy(f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A head, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in head)
                   ? iter
                   : Insert.cons(in frame, in head, in tail)
                       ? iter
                       : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A head, Func<Iter<A>> lazyTail) =>
        cons(head, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, Func<Iter<A>> lazyTail) =>
        cons(a, b, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, Func<Iter<A>> lazyTail) =>
        cons(a, b, c, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, A d, Func<Iter<A>> lazyTail) =>
        cons(a, b, c, d, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in b)
                   ? Insert.yield(in frame, in a)
                         ? iter
                         : Insert.cons(in frame, in b, in tail)
                             ? cons(a, iter)
                             : cons(a, cons(b, tail))
                   : Insert.cons(in frame, in b, in tail)
                       ? cons(a, iter)
                       : cons(a, cons(b, tail));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in c)
                   ? Insert.yield(in frame, in b)
                         ? Insert.yield(in frame, in a)
                               ? iter
                               : cons(a, cons(b, cons(c, tail)))
                         : cons(a, cons(b, cons(c, tail)))
                   : Insert.cons(in frame, in b, in tail)
                       ? cons(a, iter)
                       : cons(a, cons(b, cons(c, tail)));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, A d, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in d)
                   ? Insert.yield(in frame, in c)
                         ? Insert.yield(in frame, in b)
                               ? Insert.yield(in frame, in a)
                                     ? iter
                                     : cons(a, cons(b, cons(c, cons(d, tail))))
                               : cons(a, cons(b, cons(c, cons(d, tail))))
                         : Insert.cons(in frame, in b, in tail)
                             ? cons(a, iter)
                             : cons(a, cons(b, cons(c, cons(d, tail))))
                   : cons(a, cons(b, cons(c, cons(d, tail))));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> forever<A>(in A head)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.forever(in frame, in head)
                    ? iter
                    : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> singleton<A>(in A head)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.singleton(in frame, in head)
                   ? iter
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> lift<A>(in Iter<A> ta)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterator(in frame, in ta)
                   ? iter
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
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
        return Push.flatten(in frame, in iters)
                   ? iter
                   : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static IterBind<A, B> bind<A, B>(Func<A, Iter<B>> f) =>
        new (f);
    
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
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static IterMap<A, B, C, D, E, F, G, H> select<A, B, C, D, E, F, G, H>(Func<A, B, C, D, E, F, G, H> f) =>
        new(f);
}
