namespace IteratorPrototype;

public static partial class IteratorExtensions
{
    extension<A>(Func<Iterator<A>> ta)
    {
        public static Iterator<A> operator +(in A head, Func<Iterator<A>> tail) =>
            new(head, tail);
    }
    
    extension<A>(A head)
    {
        public Iterator<A> Cons(Func<Iterator<A>> tail) =>
            new(head, tail);
    }    
}