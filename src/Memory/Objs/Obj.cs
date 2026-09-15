using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Memory;

/// <summary>
/// Contains a struct allocated from a pool.  It automatically releases back to
/// the pool when it is no longer in use.
/// </summary>
/// <typeparam name="A"></typeparam>
[SkipLocalsInit]
public sealed class Obj<A> : BoxBase, IDisposable
    where A : class, new()
{
    readonly ObjPool<A> pool;
    internal Obj<A>? next;
    int disposed;
    A? value;

    [MethodImpl(Optimisations.InliningOnly)]
    internal Obj(ObjPool<A> pool)
    {
        value = new A();
        this.pool = pool;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    ~Obj() =>
        Free();

    [MethodImpl(Optimisations.InliningOnly)]
    public void Alloc()
    {
        // Tell the GC that we want to finalise once we're no longer in the pool
        GC.ReRegisterForFinalize(this);
        next = null;
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
        value = null!;
    }
    
    public A Value
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => value!;
    }
    
    public ref A Ref
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref value!;
    }
    
    public ref readonly A ReadonlyRef
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref value!;
    }

    public void Dispose()
    {
        if (value is not null && Interlocked.CompareExchange(ref disposed, 1, 0) == 0)
        {
            Free();
        }
    }
}
