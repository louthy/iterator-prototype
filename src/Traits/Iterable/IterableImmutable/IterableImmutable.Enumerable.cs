using System.Collections;
using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

[SkipLocalsInit]
public struct IterableImmutableEnumerable<TA, IS, A>(TA ta) : IEnumerable<A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public IterableImmutableEnumerator<TA, IS, A> GetEnumerator() =>
        new (ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
