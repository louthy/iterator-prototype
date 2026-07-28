using LanguageExt.Traits;

namespace IteratorTest.Traits;

public interface IterableK<out T>
    where T : IterableK<T>
{
    static abstract Iterator<A> Forward<A>(K<T, A> ta);
}
