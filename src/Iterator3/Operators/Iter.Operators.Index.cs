using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public static partial class IterOperators
{
    extension<A>(Iter<A>)
    {
        public static Iter<(int Index, A Item)> operator |(in Iter<A> lhs, IterIndex _) =>
            IterAction.index(in lhs);
    }
}