using System.Runtime.CompilerServices;
using IteratorTest.Traits;
using LanguageExt.Traits;

namespace IteratorTest;

// Allows abstracting over the operations of an IterableK
public abstract record VirtualTable<A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public abstract void Next(object src, ref IteratorMutable<A> next);
}

public record VirtualTable<T, TS, A> : VirtualTable<A>
    where T : IterableK<T, TS>
    where TS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public override void Next(object src, ref IteratorMutable<A> next) =>
        T.NextImmutableUntyped(src, ref next);
}

public static class VirtualTableCache<T, TS, A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public static readonly VirtualTable<A> Cache = new VirtualTable<T, TS, A>();
}