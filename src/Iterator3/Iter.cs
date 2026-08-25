using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct Iter<S, A>
{
    readonly Ops ops;
    readonly Vars vars;
    readonly ByteStack state;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    Iter(in Ops ops, in Vars vars, in ByteStack state)
    {
        this.ops = ops;
        this.vars = vars;
        this.state = state;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iter<S, A> tail)
    {
        head = default!;
        tail = this;

        var tmpVars = tail.vars;
        var frame   = tail.Frame(in tmpVars); // Copy
        
        return ops.Run(ref frame) && frame.Pop(out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    StackFrame Frame(in Vars vs) =>
        new (ref Unsafe.AsRef(in ops), ref Unsafe.AsRef(in vs), ref Unsafe.AsRef(in state));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    StackFrame Frame() =>
        new(ref Unsafe.AsRef(in ops), ref Unsafe.AsRef(in vars), ref Unsafe.AsRef(in state));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    ref Iter<S, B> Cast<B>() =>
        ref Unsafe.As<Iter<S, A>, Iter<S, B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    Iter<S, B> CopyCast<B>() =>
        Unsafe.As<Iter<S, A>, Iter<S, B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<S, B> Map<B>(Func<A, B> f)
    {
        var tb    = CopyCast<B>();
        var frame = tb.Frame();
        Push.map(ref frame, f);
        return tb;
    }

    public static Iter<IS, A> From<T, IS>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var ops   = new Ops();
        var vars  = new Vars();
        var state = new ByteStack();
        var frame = new StackFrame(ref ops, ref vars, ref state);
        
        Push.iterable<T, IS, A>(ref frame, ta);
        return new Iter<IS, A>(in ops, in vars, in state);
    }
}
