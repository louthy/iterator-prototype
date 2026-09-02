using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

public static partial class IterableImmutable
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static IS setup<T, IS, A>(in K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : unmanaged =>
        T.SetupImmutable(ta);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool step<T, IS, A>(in K<T, A> ta, in IS ts, out A head, out IS tail) 
        where T : IterableImmutable<T, IS>
        where IS : unmanaged =>
        T.StepImmutable(ta, in ts, out head, out tail);
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iterator<T, IS, A> from<T, IS, A>(in K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var s = T.SetupImmutable(ta);
        return new Iterator<T, IS, A>(ta, in s);
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iterator<A> fromWeak<T, IS, A>(in K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var              s1 = T.SetupImmutable(ta);
        ref readonly var s2 = ref Unsafe.As<IS, Space128>(ref s1);
        return new Iterator<A>(new IteratorFields<A>(ta, PureAction<T, IS, A>.Default, in s2));
    }
}