using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct Iter<A>
{
    readonly Fields fields;

    ref Fields fieldsRef
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in fields);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iter(in Fields fields) =>
        this.fields = fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iter<A> tail)
    {
        head = default!;
        tail = this;
        var frame = tail.Frame();
        frame.tops.Sync(in frame.vars.objs, in frame.vars.values); // TODO: I'd like this to not be needed
        var r = tail.fields.ops.Run(ref frame, out head);
        return r;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<B> Map<B>(Func<A, B> f) =>
        IterAction.map(f, in this);

    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<A> Prepend(A value) =>
        Iter.prepend(value, in this);
        */

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(IterScope _, Iter<A> rhs) =>
        IterAction.scope(in rhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, IterAwait _) =>
        IterAction.await(in lhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, IterPure _) =>
        IterAction.pure(in lhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, IterTake rhs) =>
        IterAction.take(rhs.amount, in lhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static StackFrame Default(out Iter<A> self)
    {
        self = default;
        var f = new StackFrame(ref self.fieldsRef);
        
        // We need an initial scope
        f.Push();
        
        // We waste a bit of space for the first global, so that 0 is a valid index
        // for the input.  But awaiting it should be considered an error.
        f.globals.Add(0xDEADBEEF);
        
        return f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static StackFrame Next(in Iter<A> current, out Iter<A> next)
    {
        next = current; // Copy
        return new StackFrame(ref next.fieldsRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static StackFrame Next<B>(in Iter<A> current, out Iter<B> next)
    {
        current.CopyCast(out next); // Copy
        return new StackFrame(ref next.fieldsRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal StackFrame Frame() =>
        new(ref fieldsRef);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal ref Iter<B> Cast<B>() =>
        ref Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void CopyCast<B>(out Iter<B> next) =>
        next = Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));

    public override string ToString() =>
        $"Iter<{Log.ty<A>()}>";
}
