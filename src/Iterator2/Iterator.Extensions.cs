using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;

namespace IteratorPrototype;

public static partial class IteratorExtensions2
{
    extension<A>(Iterator2<A> ta)
    {
        [MethodImpl(Optimisations.Default)]
        public Iterator2<A> Prepend(A head)
        {
            var iter = ta;
            ref var s1 = ref iter.Source;
            s1 = s1?.Prepend(head);
            return iter;
        }

        [MethodImpl(Optimisations.Default)]
        public Iterator2<B> Map<B>(Func<A, B> f)
        {
            ref var tb   = ref Unsafe.As<Iterator2<A>, Iterator2<B>>(ref ta);
            var     iter = tb;
            iter.Add(new MapOp<A, B>(f));
            return iter;
        }
    }
}