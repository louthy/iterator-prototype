using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IterableBaseExtensions
{
    extension<T, TS, TA, A>(IterableBase<T, TS, TA, A> ta)
        where T : IterableK<T, TS>
        where TS : struct
        where TA : IterableBase<T, TS, TA, A>
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public Iterator<T, TS, A> Forward() =>
            ta.Forward();

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> ToArray() =>
            ta.ToArray();
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IterableKEnumerator<T, TS, A> GetEnumerator() =>
            ta.GetEnumerator();
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IEnumerable<A> AsEnumerable() =>
            ta.AsEnumerable();
    }
}