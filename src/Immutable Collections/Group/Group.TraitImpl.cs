using IteratorPrototype.Iterator3;

namespace IteratorPrototype;

public class Group : Tr.Iterable<Group>
{
    public static Iter<A> Forward<A>(LT.K<Group, A> ta) =>
        ((Group<A>)ta).Values;
}