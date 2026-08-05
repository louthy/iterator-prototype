using System.Collections;
using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

[SkipLocalsInit]
public struct IterableMutableEnumerable<TA, IS, MS, A>(TA ta) : IEnumerable<A>
    where TA : class, IterableMutable<TA, IS, MS, A>
    where IS : struct
    where MS : allows ref struct
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public IterableMutableEnumerator<TA, IS, MS, A> GetEnumerator() =>
        new (ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IEnumerator<A> IEnumerable<A>.GetEnumerator()
    {
        var ts = TA.SetupImmutable(ta);
        while (TA.StepImmutable(in ta, in ts, out var head, out ts))
        {
            yield return head;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        var ts = TA.SetupImmutable(ta);
        while (TA.StepImmutable(in ta, in ts, out var head, out ts))
        {
            yield return head;
        }
    }
}
