using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype;

public static partial class IteratorExtensions2
{
    extension<A>(ref Iterator2<A> ta)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        internal void SetSource(in IteratorSource? source)
        {
            ref var s = ref Unsafe.AsRef(in ta.source);
            s = source;
        }
    }
}