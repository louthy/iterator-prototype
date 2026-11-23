#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public partial class Iter
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> from<A>(params ReadOnlySpan<A> ta) =>
        from<Arr, ArrState, A>(Arr.create(ta));
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var frame = Iter<A>.Default(out var iter);
        Push.iterable<T, IS, A>(in frame, in ta);
        return iter;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> from<A>(in Arr<A> ta) =>
        from<Arr, ArrState, A>(ta);
}
