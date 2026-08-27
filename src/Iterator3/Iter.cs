using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct Iter<A>
{
    readonly Tops tops;
    readonly Ops ops;
    readonly VStack vars;
    readonly VStack yields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iter(in Tops tops, in Ops ops, in VStack vars, in VStack yields)
    {
        this.tops = tops;
        this.ops = ops;
        this.vars = vars;
        this.yields = yields;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iter<A> tail)
    {
        head = default!;
        tail = this;
        var frame = tail.Frame();
        frame.tops.Sync(in frame.vars.objs, in frame.vars.values);  // TODO: I'd like this to not be needed
        return ops.Run(ref frame, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<B> Map<B>(Func<A, B> f) =>
        Iter.map(f, in this);

    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<A> Prepend(A value) =>
        Iter.prepend(value, in this);
        */

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, Iter<A> rhs) =>
        Iter.product(in lhs, in rhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, IterAwait rhs) =>
        Iter.awaiter(in lhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, IterPure rhs) =>
        Iter.purify(in lhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iter<A> operator |(Iter<A> lhs, IterYield rhs) =>
        Iter.yielder(in lhs);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static StackFrame Default(out Iter<A> self)
    {
        self = default;
        ref var ts = ref Unsafe.AsRef(in self.tops);
        ref var os = ref Unsafe.AsRef(in self.ops);
        ref var vs = ref Unsafe.AsRef(in self.vars);
        ref var ys = ref Unsafe.AsRef(in self.yields);
        var     f  = new StackFrame(ref ts, ref os, ref vs, ref ys);
        
        // We need an initial scope
        f.Push();
        
        return f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static StackFrame Next(in Iter<A> current, out Iter<A> next)
    {
        next = current; // Copy
        ref var ts   = ref Unsafe.AsRef(in next.tops);
        ref var os   = ref Unsafe.AsRef(in next.ops);
        ref var vs   = ref Unsafe.AsRef(in next.vars);
        ref var ys   = ref Unsafe.AsRef(in next.yields);
        return new StackFrame(ref ts, ref os, ref vs, ref ys);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static StackFrame Next<B>(in Iter<A> current, out Iter<B> next)
    {
        current.CopyCast(out next); // Copy
        ref var ts = ref Unsafe.AsRef(in next.tops);
        ref var os = ref Unsafe.AsRef(in next.ops);
        ref var vs = ref Unsafe.AsRef(in next.vars);
        ref var ys = ref Unsafe.AsRef(in next.yields);
        return new StackFrame(ref ts, ref os, ref vs, ref ys);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal StackFrame Frame() =>
        new(ref Unsafe.AsRef(in tops),
            ref Unsafe.AsRef(in ops), 
            ref Unsafe.AsRef(in vars), 
            ref Unsafe.AsRef(in yields));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal ref Iter<B> Cast<B>() =>
        ref Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void CopyCast<B>(out Iter<B> next) =>
        next = Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        
}
