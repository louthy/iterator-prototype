using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public ref struct IterableKEnumerator<T, IS, MS, A>(K<T, A> ta)
    where T : IterableK<T, IS, MS>
    where IS : struct
    where MS : allows ref struct
{
    MS state = T.SetupMutable(ta);
    A? current;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        T.StepMutable(ta, ref state, out current);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        state = T.SetupMutable(ta);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }
}