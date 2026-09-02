using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

public static partial class IterableMutable
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static MS setup<T, IS, MS, A>(K<T, A> ta)
        where T : IterableMutable<T, IS, MS>
        where IS : unmanaged 
        where MS : allows ref struct =>
        T.SetupMutable(ta);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool step<T, IS, MS, A>(in K<T, A> ta, ref MS ts, out A value) 
        where T : IterableMutable<T, IS, MS>
        where IS : unmanaged 
        where MS : allows ref struct =>
        T.StepMutable(ta, ref ts, out value);
}