using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public partial class Iter : Tr.Iterable<Iter>
{
    public static Iter<A> Forward<A>(K<Iter, A> ta) =>
        +ta;
}