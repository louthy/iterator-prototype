using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static class Iterator2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var ts = T.SetupImmutable(in ta);
        Iterator2<A> iter = default;
        
        iter.SetSource(Factory<T, IS, A>.Instance);
        iter.ops.Push(ta);
        iter.values.Push(ts);
        return iter;
    }
}

