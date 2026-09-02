using System.Collections;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public struct IterableImmutableEnumerator<T, IS, A>(in K<T, A> ta) : IEnumerator<A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    readonly K<T, A> ta = ta;
    IS state = T.SetupImmutable(ta);
    A? current;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool MoveNext() =>
        T.StepImmutable(ta, in state, out current, out state);

    [MethodImpl(Optimisations.InliningOnly)]
    public void Reset() =>
        state = T.SetupImmutable(ta);

    public A Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current!;
    }

    object IEnumerator.Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current!;
    }

    public void Dispose()
    { }
}
