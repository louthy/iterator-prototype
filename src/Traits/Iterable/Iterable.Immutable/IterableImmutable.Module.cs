using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

public static partial class IterableImmutable
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IS setup<T, IS, A>(in K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : struct =>
        T.SetupImmutable(ta);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool step<T, IS, A>(in K<T, A> ta, in IS ts, out A head, out IS tail) 
        where T : IterableImmutable<T, IS>
        where IS : struct =>
        T.StepImmutable(ta, in ts, out head, out tail);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iterator<T, IS, A> from<T, IS, A>(in K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : struct
    {
        var s = T.SetupImmutable(ta);
        return T.StepImmutable(ta, in s, out var head, out var tail) 
                   ? new Iterator<T, IS, A>(in head, ta, in tail) 
                   : default;
    }    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iterator<A> fromWeak<T, IS, A>(in K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : struct
    {
        var s = T.SetupImmutable(in ta);
        if (T.StepImmutable(in ta, in s, out var head, out var tail))
        {
            ref readonly var t = ref Unsafe.As<IS, Space128>(ref tail);
            return new Iterator<A>(in head, ta, VirtualTableCache<T, IS, A>.Cache, in t);
        }
        else
        {
            return default;
        }
    }
}