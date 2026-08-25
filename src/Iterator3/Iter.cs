using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly struct Iter<A>
{
    readonly Ops ops;
    readonly Vars vars;
    readonly ByteStack state;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iter(in Ops ops, in Vars vars, in ByteStack state)
    {
        this.ops = ops;
        this.vars = vars;
        this.state = state;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iter<A> tail)
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
    ref Iter<B> Cast<B>() =>
        ref Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    Iter<B> CopyCast<B>() =>
        Unsafe.As<Iter<A>, Iter<B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<B> Map<B>(Func<A, B> f)
    {
        var tb    = CopyCast<B>();
        var frame = tb.Frame();
        Push.map(ref frame, f);
        return tb;
    }
}
