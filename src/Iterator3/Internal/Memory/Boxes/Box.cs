using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Iterator3.Internal.Memory;

/// <summary>
/// Contains a struct allocated from a pool.  It automatically releases back to
/// the pool when it is no longer in use.
/// </summary>
/// <typeparam name="A"></typeparam>
[SkipLocalsInit]
public sealed class Box<A> : BoxBase
    where A : struct
{
    readonly BoxPool<A> pool;
    internal Box<A>? next;
    A value;
    
    [MethodImpl(Optimisations.InliningOnly)]
    internal Box(BoxPool<A> pool) =>
        this.pool = pool;

    [MethodImpl(Optimisations.InliningOnly)]
    ~Box() =>
        Free();

    [MethodImpl(Optimisations.InliningOnly)]
    public void Alloc(in A value)
    {
        // Tell the GC that we want to finalise once we're no longer in the pool
        GC.ReRegisterForFinalize(this);
        this.value = value;
        //next = null;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override void VirtualFree() =>
        Free();

    /// <summary>
    /// Release back to the pool
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public void Free()
    {
        // We don't need the finaliser to run if we're in the pool
        GC.SuppressFinalize(this);
        pool.Free(this);
        value = default!;
    }
    
    public A Value
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => value;
    }
    
    public ref A Ref
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref value;
    }
    
    public ref readonly A ReadonlyRef
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref value;
    }
}
