using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public static partial class IterableK
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void setupMutable<T, IS, MS, A>(K<T, A> ta, out MS state)
        where T : IterableK<T, IS, MS>
        where IS : struct 
        where MS : allows ref struct =>
        T.SetupMutable(ta, out state);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool stepMutable<T, IS, MS, A>(K<T, A> ta, ref MS ts, out A value) 
        where T : IterableK<T, IS, MS>
        where IS : struct 
        where MS : allows ref struct =>
        T.StepMutable(ta, ref ts, out value);
}