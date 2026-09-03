using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Iterator3.Internal.Collections;

/// <summary>
/// Contains a struct allocated from a pool.  It automatically releases back to
/// the pool when it is no longer in use.
/// </summary>
/// <typeparam name="A"></typeparam>
public class Box<A>
    where A : struct
{
    internal readonly BoxPool<A> pool;
    internal Box<A>? next;
    A value;

    [MethodImpl(Optimisations.InliningOnly)]
    internal Box(BoxPool<A> pool, Box<A>? next)
    {
        this.pool = pool;
        this.next = next;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    ~Box() =>
        // Release back to the pool
        pool.Free(this);

    [MethodImpl(Optimisations.InliningOnly)]
    public void OnAlloc(in A value)
    {
        // Tell the GC that we want to finalise once we're no longer in the pool
        GC.ReRegisterForFinalize(this);
        this.value = value;
        next = null;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void OnFree(Box<A> head)
    {
        // We don't need the finaliser to run if we're in the pool
        GC.SuppressFinalize(this);
        value = default!;
        next = head;
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
