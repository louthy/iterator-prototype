using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public static class IterableK
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static TS setup<T, TS, A>(K<T, A> ta)
        where T : IterableK<T, TS>
        where TS : struct =>
        T.Setup(ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool step<T, TS, A>(K<T, A> ta, ref TS ts, out A value) 
        where T : IterableK<T, TS>
        where TS : struct =>
        T.StepMutable(ta, ref ts, out value);
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Iterator<A> fromIterable<T, TS, A>(K<T, A> ta)
        where T : IterableK<T, TS>
        where TS : struct
    {
        var s = T.Setup(ta);
        if (T.StepImmutable(ta, in s, out var i1))
        {
            ref var i2 = ref Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref i1);
            return i2;
        }
        else
        {
            return default;
        }
    }
}