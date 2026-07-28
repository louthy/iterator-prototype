using LanguageExt.Traits;

namespace IteratorTest.Traits;

public interface IterableK<out T, TS> : IterableK<T>
    where T : IterableK<T, TS>
    where TS : struct
{
    static abstract TS Setup<A>(K<T, A> ta);
    static abstract bool Step<A>(ref TS ts, out A value);
}
