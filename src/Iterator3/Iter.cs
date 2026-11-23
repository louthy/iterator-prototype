using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
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
            cons = new Cons<A>(x, xs);
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

    [MethodImpl(Optimisations.Default)]
    public Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, in this);

    [MethodImpl(Optimisations.Default)]
    public Iter<B> Bind<B>(Func<A, Iter<B>> f) =>
        IterAction.bind(in this, f);

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
    internal ref Iter<B> Cast<B>() =>
        ref Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(Optimisations.Default)]
    void CopyCast<B>(out Iter<B> next) =>
        next = Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));

    public override string ToString() =>
        $"Iter<{Log.ty<A>()}>";
}
