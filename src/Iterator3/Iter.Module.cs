using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static class Iter
{
    public static Iter<IS, A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        Iter<IS, A>.From<T, IS>(ta);
}
