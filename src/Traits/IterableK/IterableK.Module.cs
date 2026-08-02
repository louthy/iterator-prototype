using LanguageExt.Traits;

namespace IteratorTest.Traits;

public static partial class IterableK
{
    /// <summary>
    /// Iterates from the first item in the structure to the last.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<A> forward<T, A>(K<T, A> ta)
        where T : IterableK<T> =>
        T.Forward(ta);
}