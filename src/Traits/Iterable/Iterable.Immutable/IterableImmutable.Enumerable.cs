using System.Collections;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public readonly struct IterableImmutableEnumerable<T, IS, A>(K<T, A> ta) : IEnumerable<A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IterableImmutableEnumerator<T, IS, A> GetEnumerator() =>
        new (ta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator<A> IEnumerable<A>.GetEnumerator() =>
        GetEnumerator();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
