using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public struct IterableEnumerator<T, A>(K<T, A> ta)
    where T : Iterable<T>
{
    Iter<A> iter = T.Forward(ta);
    A? current;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool MoveNext() =>
        iter.TryRef(out current);

    [MethodImpl(Optimisations.InliningOnly)]
    public void Reset() =>
        iter = T.Forward(ta);

    public A Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current!;
    }
}