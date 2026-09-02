using System.Collections;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public readonly struct IterableImmutableEnumerable<T, IS, A>(K<T, A> ta) : IEnumerable<A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(Optimisations.InliningOnly)]
    public IterableImmutableEnumerator<T, IS, A> GetEnumerator() =>
        new (ta);

    [MethodImpl(Optimisations.InliningOnly)]
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    [MethodImpl(Optimisations.InliningOnly)]
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
