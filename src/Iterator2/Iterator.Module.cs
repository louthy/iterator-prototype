using IteratorPrototype.Internal.Sources;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static class Iterator2
{
    [MethodImpl(Optimisations.Default)]
    public static Iterator2<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var ts = T.SetupImmutable(in ta);
        Iterator2<A> iter = default;
        
        ref var stack  = ref Unsafe.AsRef(in iter.stack);
        ref var frame  = ref stack.Push();
        ref var source = ref Unsafe.AsRef(in frame.source);
        ref var objs   = ref Unsafe.AsRef(in frame.objs);
        ref var values = ref Unsafe.AsRef(in frame.values);
        
        source = IterableSource<T, IS, A>.Instance;
        objs.Push(ta);
        values.Push(ts);
        
        return iter;
    }
}

