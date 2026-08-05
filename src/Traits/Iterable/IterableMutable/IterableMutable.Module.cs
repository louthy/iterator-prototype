using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

public static partial class IterableMutable
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static MS setup<TA, IS, MS, A>(TA ta)
        where TA : class, IterableMutable<TA, IS, MS, A>
        where IS : struct 
        where MS : allows ref struct =>
        TA.SetupMutable(ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool step<TA, IS, MS, A>(in TA ta, ref MS ts, out A value) 
        where TA : class, IterableMutable<TA, IS, MS, A>
        where IS : struct 
        where MS : allows ref struct =>
        TA.StepMutable(ta, ref ts, out value);
}