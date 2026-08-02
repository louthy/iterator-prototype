using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IterableBaseExtensions
{
    extension<T, IS, MS, TA, A>(IterableBase<T, IS, MS, TA, A> ta)
        where T : IterableK<T, IS, MS>
        where IS : struct
        where MS : allows ref struct
        where TA : IterableBase<T, IS, MS, TA, A>
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public Iterator<T, IS, A> Forward() =>
            ta.Forward();

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> AsSpan() =>
            ta.AsSpan();
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IterableKEnumerator<T, IS, MS, A> GetEnumerator() =>
            ta.GetEnumerator();
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IEnumerable<A> AsEnumerable() =>
            ta.AsEnumerable();
    }
}