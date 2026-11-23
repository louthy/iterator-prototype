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
}