using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public struct IterableKEnumerator<T, TS, A>(K<T, A> ta)
    where T : IterableK<T, TS>
    where TS : struct
{
    TS foldState = T.Setup(ta);
    A? current;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() =>
        T.StepMutable(ta, ref foldState, out current);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Reset() =>
        foldState = T.Setup(ta);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => current!;
    }
}