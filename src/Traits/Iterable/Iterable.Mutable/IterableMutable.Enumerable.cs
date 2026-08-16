using System.Collections;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public readonly struct IterableMutableEnumerable<T, IS, MS, A>(K<T, A> ta) : IEnumerable<A>
    where T : IterableMutable<T, IS, MS>
    where IS : struct
    where MS : allows ref struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IterableMutableEnumerator<T, IS, MS, A> GetEnumerator() =>
        new (ta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator<A> IEnumerable<A>.GetEnumerator()
    {
        var ts = T.SetupImmutable(ta);
        while (T.StepImmutable(in ta, in ts, out var head, out ts))
        {
            yield return head;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        var ts = T.SetupImmutable(ta);
        while (T.StepImmutable(in ta, in ts, out var head, out ts))
        {
            yield return head;
        }
    }
}
