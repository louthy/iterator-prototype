#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

[SkipLocalsInit]
public struct IterableEnumerator<TA, IA, A>([NotNull] TA ta)
    where TA : Iterable<TA, IA, A>
    where IA : IIterator<IA, A>
{
    IA iter = TA.Forward(ta);
    A? current;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        iter.TryGetValue(out current, out iter);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        iter = TA.Forward(ta);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }
}