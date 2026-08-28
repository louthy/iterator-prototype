using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static class IterOperators
{
    extension<A, B>(Iter<A>)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Iter<B> operator |(Iter<A> lhs, IterMap<A, B> rhs) =>
            IterAction.map(rhs.f, in lhs);
    }
}