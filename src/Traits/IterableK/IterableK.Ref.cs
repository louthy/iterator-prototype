using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

public interface IterableK<out T, TS> : IterableK<T>
    where T : IterableK<T, TS>
    where TS : struct
{
    static abstract TS Setup<A>(K<T, A> ta);
    static abstract bool Step<A>(K<T, A> ta, ref TS ts, out A value);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Iterator<A> IterableK<T>.Forward<A>(K<T, A> ta) =>
        IterableK.fromIterable<T, TS, A>(ta);    
}
