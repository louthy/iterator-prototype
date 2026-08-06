using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

// Allows abstracting over the operations of an IterableK
public abstract record VirtualTable<A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public abstract void Next(in Iterable<A> src, ref IteratorMutable<A> next);
}

public record VirtualTable<TA, IS, A> : VirtualTable<A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public override void Next(in Iterable<A> src, ref IteratorMutable<A> next)
    {
        ref var ta    = ref Unsafe.As<Iterable<A>, TA>(ref Unsafe.AsRef(in src));
        ref var state = ref Unsafe.As<IteratorMutable<A>, IteratorMutable<TA, IS, A>>(ref next);
        TA.NextImmutable(in ta, ref state);
    }
}

public static class VirtualTableCache<TA, IS, A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    public static readonly VirtualTable<A> Cache = new VirtualTable<TA, IS, A>();
}