using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public interface IterableK<T, TS> : IterableK<T>
    where T : IterableK<T, TS>
    where TS : struct
{
    static abstract TS Setup<A>(K<T, A> ta);
    
    /// <summary>
    /// Used for high-performance, mutable, iteration.
    /// </summary>
    static abstract bool StepMutable<A>(K<T, A> ta, ref TS ts, out A value);

    /// <summary>
    /// Used for high-performance, immutable, iteration.
    /// </summary>
    static abstract bool StepImmutable<A>(K<T, A> ta, in TS ts, out Iterator<T, TS, A> value);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Iterator<A> IterableK<T>.Forward<A>(K<T, A> ta) =>
        IterableK.fromIterable<T, TS, A>(ta);    
}
