using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public struct IterableEnumerator<T, A>(K<T, A> ta)
    where T : Iterable<T>
{
    Iterator<A> iter = T.Forward(ta);
    A? current;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool MoveNext() =>
        iter.TryGetValue(out current, out iter);

    [MethodImpl(Optimisations.InliningOnly)]
    public void Reset() =>
        iter = T.Forward(ta);

    public A Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current!;
    }
}