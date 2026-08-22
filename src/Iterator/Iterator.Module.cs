using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public class Iterator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iterator<A> fromWeak<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var s = T.SetupImmutable(ta);
        var i = new Iterator<T, IS, A>(ta, PureAction<T, IS, A>.Default, in s);
        return Unsafe.As<Iterator<T, IS, A>, Iterator<A>>(ref i);
    }     
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iterator<T, IS, A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var s = T.SetupImmutable(ta);
        return new Iterator<T, IS, A>(ta, in s);
    }    
}