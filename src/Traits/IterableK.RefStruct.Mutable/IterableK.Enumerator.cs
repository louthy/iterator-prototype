using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public ref struct IterableKEnumerator<T, IS, MS, A>
    where T : IterableK<T, IS, MS>
    where IS : struct
    where MS : allows ref struct
{
    readonly bool valid;
    readonly K<T, A> ta;
    MS state;
    A? current;

    public IterableKEnumerator(K<T, A> ta)
    {
        valid = true;
        this.ta = ta;
        T.SetupMutable(ta, out state);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        valid && T.StepMutable(ta, ref state, out current);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        T.SetupMutable(ta, out state);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }
}