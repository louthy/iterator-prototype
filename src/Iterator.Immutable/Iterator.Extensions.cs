using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IteratorExtensions
{
    extension<IS, TA, A>(Func<Iterator<TA, IS, A>> ta)
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct
    {
        public static Iterator<TA, IS, A> operator +(in A head, Func<Iterator<TA, IS, A>> tail) =>
            new(head, tail);
    }
    
    extension<IS, TA, A>(A head)
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct
    {
        public Iterator<TA, IS, A> Cons(Func<Iterator<TA, IS, A>> tail) =>
            new(head, tail);
    }    
}