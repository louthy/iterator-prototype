using System.Collections;
using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

[SkipLocalsInit]
public struct IterableImmutableEnumerator<TA, IS, A>(in TA ta) : IEnumerator<A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    readonly TA ta = ta;
    IS state = TA.SetupImmutable(ta);
    A? current;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        TA.StepImmutable(ta, in state, out current, out state);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        state = TA.SetupImmutable(ta);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }

    object IEnumerator.Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }

    public void Dispose()
    { }
}
