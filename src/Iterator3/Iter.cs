using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.ClassInstances;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

[Union]
[SkipLocalsInit]
public readonly struct Iter<A> : IUnion, K<Iter, A>
{
    readonly Fields fields;

    ref Fields fieldsRef
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AsRef(in fields);
    }

    public object Value =>
        TryGetValue(out var x, out var xs)
            ? new Cons<Iter, A>(x, xs)
            : Nil.DefaultObj;

    // ReSharper disable once UnusedParameter.Local
    public Iter(Nil _)
    {
        fields = default;
        var f = new StackFrame(in fields);
        
        // We need an initial scope
        f.Push();
        
        // We waste a bit of space for the first global, so that 0 is a valid index
        // for the input.  But awaiting it should be considered an error.
        f.globals.AddMutable(0xDeadBeef);
    }
    
    public Iter(Cons<A> cons)
    {
        fields = Iter.cons(cons.Head, cons.Tail).fields;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    internal Iter(in Fields fields) =>
        this.fields = fields;

    /// <summary>
    /// Returns `true` if the iterable is empty, `false` otherwise. `Nil` is a unit-value that makes pattern-matching
    /// more declarative.
    /// </summary>
    /// <param name="nil">Nil</param>
    /// <returns>`true` if the iterable is empty, `false` otherwise</returns>
    [Pure]
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return !TryGetValue(out _, out _);
    }

    /// <summary>
    /// Returns the first element of the iterable and the rest of the iterable in a `Cons` structure.
    /// </summary>
    /// <param name="cons">Cons structure containing head and tail values</param>
    /// <returns>`true` if it was possible to extract a head value.</returns>
    [Pure]
    public bool TryGetValue(out Cons<A> cons)
    {
        if (TryGetValue(out var x, out var xs))
        {
            cons = new Cons<A>(in x, in xs);
            return true;
        }
        else
        {
            cons = default;
            return false;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool TryGetValue(out A head, out Iter<A> tail)
    {
        tail = this;
        var frame = tail.Frame();
        //Log.scope();
        var r = OpsVM<A>.Run(in frame, out head);
        //Log.descope();
        return r;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    internal static bool TryRef(ref Iter<A> tail, out A head)
    {
        var frame = tail.Frame();
        //Log.scope();
        return OpsVM<A>.Run(in frame, out head); 
        //Log.descope();
    }

    /// <summary>
    /// Functor map
    /// </summary>
    /// <param name="f">Mapping function</param>
    /// <typeparam name="B">Resulting element type</typeparam>
    /// <returns>Projected lazy iterator</returns>
    public Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, in this);

    /// <summary>
    /// Monad bind
    /// </summary>
    /// <param name="f">Bind function</param>
    /// <typeparam name="B">Resulting element type</typeparam>
    /// <returns>Projected lazy iterator</returns>
    public Iter<B> Bind<B>(Func<A, Iter<B>> f) =>
        IterAction.bind(in this, f);

    /// <summary>
    /// Monad bind followed by a projection function
    /// </summary>
    /// <param name="bind">Bind function</param>
    /// <param name="project">Projection function</param>
    /// <typeparam name="B">Intermediate element type</typeparam>
    /// <typeparam name="C">Resulting element type</typeparam>
    /// <returns>Projected lazy iterator</returns>
    public Iter<C> SelectMany<B, C>(Func<A, Iter<B>> bind, Func<A, B, C> project) =>
        IterAction.selectMany(in this, bind, project);

    /// <summary>
    /// Allow values through the iterator if they return `true` when passed to the predicate function provided.
    /// </summary>
    /// <param name="f">Predicate function</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<A> Filter(Func<A, bool> f) =>
        IterAction.filter(f, in this);

    /// <summary>
    /// Allow values through the iterator if they return `true` when passed to the predicate function provided.
    /// </summary>
    /// <param name="f">Predicate function</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<A> Where(Func<A, bool> f) =>
        Filter(f);

    /// <summary>
    /// Allow values through the iterator if they return `Some` when passed to the choice function provided.
    /// </summary>
    /// <param name="f">Choice function</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<B> Choose<B>(Func<A, LE.Option<B>> f) =>
        IterAction.choose(f, in this);

    /// <summary>
    /// Cast the values in the iterator to another type. If the cast succeeds, the value will yield, otherwise
    /// the value will be dropped. 
    /// </summary>
    /// <typeparam name="B">Type to cast the values to</typeparam>
    /// <returns>Projected lazy iterator</returns>
    public Iter<B> Cast<B>() =>
        Choose<B>(x => x is B b ? b : default);

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<A> Distinct(params ReadOnlySpan<A> seen) =>
        IterAction.distinct<EqDefault<A>, A>(seen, in this);

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<A> Distinct<EqA>(params ReadOnlySpan<A> seen)
        where EqA : Eq<A> =>
        IterAction.distinct<EqA, A>(seen, in this);

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="key">Function that maps from the element-type to a key for distinct testing.</param>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<A> DistinctBy<Key>(Func<A, Key> key, params ReadOnlySpan<Key> seen) =>
        IterAction.distinct<EqDefault<Key>, Key, A>(key, seen, in this);

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="key">Function that maps from the element-type to a key for distinct testing.</param>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <returns>Projected lazy iterator</returns>
    public Iter<A> DistinctBy<EqKey, Key>(Func<A, Key> key, params ReadOnlySpan<Key> seen)
        where EqKey : Eq<Key> =>
        IterAction.distinct<EqKey, Key, A>(key, seen, in this);

    /*
    [MethodImpl(Optimisations.Default)]
    public Iter<A> Prepend(A value) =>
        Iter.prepend(value, in this);
        */

    [MethodImpl(Optimisations.Default)]
    public static IterCombine<A> operator +(in Iter<A> lhs, in Iter<A> rhs) =>
        new(lhs, rhs);

    [MethodImpl(Optimisations.Default)]
    public static IterCons<A> operator +(in A lhs, in Iter<A> rhs) =>
        new (in lhs, in rhs);

    [MethodImpl(Optimisations.Default)]
    public static Iter<A> operator +(in Iter<A> lhs, in A rhs) =>
        Iter.combine(lhs, Iter.singleton(rhs));
    
    [MethodImpl(Optimisations.Default)]
    public static Iter<A> operator |(IterScope _, Iter<A> rhs) =>
        IterAction.scope(in rhs);

    [MethodImpl(Optimisations.Default)]
    public static Iter<A> operator |(Iter<A> lhs, IterPure _) =>
        IterAction.pure(in lhs);

    [MethodImpl(Optimisations.Default)]
    public static Iter<A> operator |(Iter<A> lhs, IterTake rhs) =>
        IterAction.take(rhs.amount, in lhs);

    public static implicit operator Iter<A>(IterEmpty _)
    {
        Default(out var self);
        return self;
    }

    public static implicit operator Iter<A>(Nil _)
    {
        Default(out var self);
        return self;
    }

    [MethodImpl(Optimisations.Default)]
    internal static StackFrame Default(out Iter<A> self)
    {
        self = default;
        var f = new StackFrame(in self.fieldsRef);
        
        // We need an initial scope
        f.Push();
        
        // We waste a bit of space for the first global, so that 0 is a valid index
        // for the input.  But awaiting it should be considered an error.
        f.globals.AddMutable(0xDeadBeef);
        
        return f;
    }

    [MethodImpl(Optimisations.Default)]
    internal static StackFrame Next(in Iter<A> current, out Iter<A> next)
    {
        next = current; // Copy
        return new StackFrame(in next.fieldsRef);
    }

    [MethodImpl(Optimisations.Default)]
    internal static StackFrame Next<B>(in Iter<A> current, out Iter<B> next)
    {
        current.CopyCast(out next); // Copy
        return new StackFrame(in next.fieldsRef);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    StackFrame Frame() =>
        new(in fieldsRef);

    [MethodImpl(Optimisations.Default)]
    internal ref Iter<B> ForceCast<B>() =>
        ref Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(Optimisations.Default)]
    void CopyCast<B>(out Iter<B> next) =>
        next = Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));

    public override string ToString() =>
        $"Iter<{Log.ty<A>()}>";
}
