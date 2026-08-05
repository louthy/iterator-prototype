using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IterableMutableExtensions
{
    extension<TA, IS, MS, A>(IterableMutable<TA, IS, MS, A> ta)
        where TA : class, IterableMutable<TA, IS, MS, A>
        where IS : struct
        where MS : allows ref struct
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public Iterator<TA, IS, A> Forward() =>
            TA.Forward(ta.Self);

        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        public IterableMutableEnumerator<TA, IS, MS, A> GetEnumerator() =>
            new (ta.Self);
    }
}