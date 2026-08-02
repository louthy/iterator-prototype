using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IterableBaseExtensions
{
    extension<T, IS, TA, A>(IterableBase<T, IS, TA, A> ta)
        where T : IterableK<T, IS>
        where IS : struct
        where TA : IterableBase<T, IS, TA, A>
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public Iterator<T, IS, A> Forward() =>
            ta.Forward();

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> AsSpan() =>
            ta.AsSpan();
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IterableKEnumerator<T, IS, A> GetEnumerator() =>
            ta.GetEnumerator();
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IEnumerable<A> AsEnumerable() =>
            ta.AsEnumerable();
    }
}