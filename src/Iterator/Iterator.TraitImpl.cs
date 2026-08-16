using LanguageExt.Traits;

namespace IteratorPrototype;

public partial class Iterator : Tr.Iterable<Iterator>
{
    public static Iterator<A> Forward<A>(K<Iterator, A> ta) =>
        ta is Iterator<A> ita
            ? ita
            : throw new InvalidCastException(nameof(ta));
}