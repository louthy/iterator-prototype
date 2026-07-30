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
    public static bool stepMutable<T, TS, A>(K<T, A> ta, ref TS ts, out A value) 
        where T : IterableK<T, TS>
        where TS : struct =>
        T.StepMutable(ta, ref ts, out value);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static bool stepImmutable<T, TS, A>(K<T, A> ta, in TS ts, out Iterator<T, TS, A> next) 
        where T : IterableK<T, TS>
        where TS : struct =>
        T.StepImmutable(ta, in ts, out next);
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Iterator<A> fromIterable<T, TS, A>(K<T, A> ta)
        where T : IterableK<T, TS>
        where TS : struct
    {
        var s = T.Setup(ta);
        return T.StepImmutable(ta, in s, out var i1) 
                   ? Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref i1) 
                   : default;
    }
}