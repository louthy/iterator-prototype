using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype;

public static partial class IteratorExtensions2
{
    extension<A>(Iterator2<A> ta)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Prepend(A head)
        {
            Iterator2<A> iter = default;
            ta.CopyTo(ref iter);
            ref var s1 = ref Unsafe.AsRef(in iter.source);
            s1 = ((IteratorSource<A>?)iter.source)?.Prepend(head);
            return iter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<B> Map<B>(Func<A, B> f)
        {
            Iterator2<B> iter = default;
            ref var      tb   = ref Unsafe.As<Iterator2<A>, Iterator2<B>>(ref ta);
            tb.CopyTo(ref iter);
            ref var ops = ref Unsafe.AsRef(in iter.ops);
            ops.Add(new MapOp<A, B>(f));
            return iter;
        }
    }
}