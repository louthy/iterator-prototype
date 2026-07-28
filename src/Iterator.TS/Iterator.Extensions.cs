using IteratorTest.Traits;

namespace IteratorTest;

public static partial class IteratorExtensions
{
    extension<T, TS, A>(Func<Iterator<T, TS, A>> ta)
        where T : IterableK<T, TS>
        where TS : struct
    {
        public static Iterator<T, TS, A> operator +(in A head, Func<Iterator<T, TS, A>> tail) =>
            new(head, tail);
    }
    
    extension<T, TS, A>(A head)
        where T : IterableK<T, TS>
        where TS : struct
    {
        public Iterator<T, TS, A> Cons(Func<Iterator<T, TS, A>> tail) =>
            new(head, tail);
    }    
}