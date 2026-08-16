using System.Collections;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public struct IterableImmutableEnumerator<T, IS, A>(in K<T, A> ta) : IEnumerator<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    readonly K<T, A> ta = ta;
    IS state = T.SetupImmutable(ta);
    A? current;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        T.StepImmutable(ta, in state, out current, out state);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        state = T.SetupImmutable(ta);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => current!;
    }

    object IEnumerator.Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => current!;
    }

    public void Dispose()
    { }
}
