using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public static partial class IterableK
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static TS setupImmutable<T, TS, A>(K<T, A> ta)
        where T : IterableK<T, TS>
        where TS : struct =>
        T.SetupImmutable(ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool stepImmutable<T, TS, A>(K<T, A> ta, in TS ts, out A head, out TS tail) 
        where T : IterableK<T, TS>
        where TS : struct =>
        T.StepImmutable(ta, in ts, out head, out tail);
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Iterator<A> fromIterable<T, TS, A>(K<T, A> ta)
        where T : IterableK<T, TS>
        where TS : struct
    {
        var s = T.SetupImmutable(ta);
        if (T.StepImmutable(ta, in s, out var head, out var tail))
        {
            ref readonly var t = ref Unsafe.As<TS, Space128>(ref tail);
            return new Iterator<A>(in head, ta, VirtualTableCache<T, TS, A>.Cache, in t);
        }
        else
        {
            return default;
        }
    }
        
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Iterator<T, TS, A> fromIterableStrong<T, TS, A>(K<T, A> ta)
        where T : IterableK<T, TS>
        where TS : struct
    {
        var s = T.SetupImmutable(ta);
        return T.StepImmutable(ta, in s, out var head, out var tail) 
                   ? new Iterator<T, TS, A>(in head, ta, in tail) 
                   : default;
    }
}