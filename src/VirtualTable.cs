using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

// Allows abstracting over the operations of an IterableK
public abstract record VirtualTable<A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public abstract void Next(in object src, ref IteratorFieldsMutable<A> next);
}

public record VirtualTable<T, IS, A> : VirtualTable<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public override void Next(in object src, ref IteratorFieldsMutable<A> next)
    {
        ref readonly var ta    = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in src));
        ref var          state = ref Unsafe.As<IteratorFieldsMutable<A>, IteratorFieldsMutable<T, IS, A>>(ref next);
        T.Next(in ta, ref state);
    }
}

public static class VirtualTableCache<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public static readonly VirtualTable<A> Cache = new VirtualTable<T, IS, A>();
}