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
    /// Add an index to the element stream
    /// </summary>
    public static readonly IterIndex index =
        default;
    
    /// <summary>
    /// Take a number of elements
    /// </summary>
    /// <param name="amount">Number of elements to take</param>
    [MethodImpl(Optimisations.InliningOnly)]
    public static IterTake take(int amount) => 
        new (amount);
    
    /// <summary>
    /// Skip a number of elements
    /// </summary>
    /// <param name="amount">Number of elements to skip</param>
    [MethodImpl(Optimisations.InliningOnly)]
    public static IterSkip skip(int amount) => 
        new (amount);

    /// <summary>
    /// Lift a function into an iterator, such that it is evaluated on-demand, rather than now.
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> lazy<A>(Func<Iter<A>> f) =>
        IterAction.lazy(f);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> forever<A>(in A value)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.forever(in frame, in value)
                    ? iter
                    : throw new ArgumentException("The size of `value` is too large for an `Iter` structure");
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> singleton<A>(in A value)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.singleton(in frame, in value)
                   ? iter
                   : throw new ArgumentException("The size of `value` is too large for an `Iter` structure");
    }

    /// <summary>
    /// Lifts an `Iter` into a 'variable' within a new `Iter` structure. 
    /// </summary>
    /// <remarks>
    /// The variable will be a serialised as a `struct` with 'managed' fields; and so it will cause
    /// the passed `Iter` to be boxed.
    ///
    /// The reason we might want this is that the `Iter` structure can become 'full' when its internal
    /// actions and variables take up more space within the fixes sized `Iter` struct.  This allows
    /// the whole structure to become a reference and gives more space within a new `Iter` to do
    /// additional operations on it.
    /// </remarks>
    /// <param name="ta">Structure to lift</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> lift<A>(in Iter<A> ta)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterator(in frame, in ta)
                   ? iter
                   : throw new InvalidOperationException();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> flatten<A>(params ReadOnlySpan<Iter<A>> ts)
    {
        var iters = many(ts);
        var frame = Iter<A>.Default(out var iter);
        return Push.flatten(in frame, in iters)
                   ? iter
                   : throw new InvalidOperationException();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> flatten<A>(Iter<Iter<A>> ts)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.flatten(in frame, in ts)
                   ? iter
                   : throw new InvalidOperationException();
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
