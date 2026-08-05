#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

[SkipLocalsInit]
public ref struct IterableMutableEnumerator<TA, IS, MS, A>([NotNull] TA ta)
    where TA : class, IterableMutable<TA, IS, MS, A>
    where IS : struct
    where MS : allows ref struct
{
    readonly bool valid = true;
    MS state = TA.SetupMutable(ta);
    A? current;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        valid && TA.StepMutable(ta, ref state, out current);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        state = TA.SetupMutable(ta);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }
}