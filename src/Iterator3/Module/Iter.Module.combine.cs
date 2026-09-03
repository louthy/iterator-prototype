using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> combine<A>(in Iter<A> tx, in Iter<A> ty)
    {
        var frame = tx.Next(out var tz);
        return Push.iterators(ref frame, in tx, in ty)
                   ? tz
                   : default;
    }
}
