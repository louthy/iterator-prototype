using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IterableImmutableExtensions
{
    extension<TA, IS, A>(IterableImmutable<TA, IS, A> ta)
        where IS : struct
        where TA : class, IterableImmutable<TA, IS, A>
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public Iterator<TA, IS, A> Forward() =>
            TA.Forward(ta.Self);

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<A> AsSpan() =>
            TA.AsSpan(ta.Self);

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IEnumerable<A> AsEnumerable() =>
            TA.AsEnumerable(ta.Self);
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IterableImmutableEnumerator<TA, IS, A> GetEnumerator() =>
            TA.GetEnumerator(ta.Self);

        public IterableImmutableEnumerable<TA, IS, A> yield
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
            get => new(ta.Self);
        }
    }
}