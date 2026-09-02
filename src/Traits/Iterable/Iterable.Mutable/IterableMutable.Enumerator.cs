using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

[SkipLocalsInit]
public ref struct IterableMutableEnumerator<T, IS, MS, A>(K<T, A> ta)
    where T : IterableMutable<T, IS, MS>
    where IS : unmanaged
    where MS : allows ref struct
{
    readonly bool valid = true;
    MS state = T.SetupMutable(ta);
    A? current;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool MoveNext() =>
        valid && T.StepMutable(ta, ref state, out current);

    [MethodImpl(Optimisations.InliningOnly)]
    public void Reset() =>
        state = T.SetupMutable(ta);

    public IterableMutableEnumerator<T, IS, MS, A> GetEnumerator() =>
        this;

    public A Current
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => current!;
    }
}