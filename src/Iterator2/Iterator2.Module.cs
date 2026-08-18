using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public class Iterator2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iterator2<A> fromWeak<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : struct
    {
        var s = T.SetupImmutable(ta);
        return new Iterator2<A>(ta, IdAction<T, IS, A>.Default, in Unsafe.As<IS, Space128>(ref s));
    }     
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iterator2<T, IS, A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : struct
    {
        var s = T.SetupImmutable(ta);
        return new Iterator2<T, IS, A>(ta, in s);
    }    
}