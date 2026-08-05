using System.Runtime.CompilerServices;

namespace IteratorTest.Traits;

public static partial class IterableImmutable
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static IS setup<TA, IS, A>(in TA ta)
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        TA.SetupImmutable(ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool step<TA, IS, A>(in TA ta, in IS ts, out A head, out IS tail) 
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        TA.StepImmutable(ta, in ts, out head, out tail);
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Iterator<A> from<TA, IS, A>(in TA ta)
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct
    {
        var s = TA.SetupImmutable(ta);
        if (TA.StepImmutable(ta, in s, out var head, out var tail))
        {
            ref readonly var t = ref Unsafe.As<IS, Space128>(ref tail);
            return new Iterator<A>(in head, ta, VirtualTableCache<TA, IS, A>.Cache, in t);
        }
        else
        {
            return default;
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Iterator<TA, IS, A> fromStrong<TA, IS, A>(in TA ta)
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct
    {
        var s = TA.SetupImmutable(ta);
        return TA.StepImmutable(ta, in s, out var head, out var tail) 
                   ? new Iterator<TA, IS, A>(in head, ta, in tail) 
                   : default;
    }
}