using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IteratorExtensions
{
    extension<IS, T, A>(Func<Iterator<T, IS, A>> ta)
        where T : IterableImmutable<T, IS>
        where IS : struct
    {
        public static Iterator<T, IS, A> operator +(in A head, Func<Iterator<T, IS, A>> tail) =>
            new(head, tail);
    }
    
    extension<IS, T, A>(A head)
        where T : IterableImmutable<T, IS>
        where IS : struct
    {
        public Iterator<T, IS, A> Cons(Func<Iterator<T, IS, A>> tail) =>
            new(head, tail);
    }    
}