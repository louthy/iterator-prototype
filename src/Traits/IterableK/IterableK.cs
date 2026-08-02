using LanguageExt.Traits;

namespace IteratorTest.Traits;

/// <summary>
/// Trait for structures that can be enumerated
/// </summary>
/// <typeparam name="T">Trait type</typeparam>
public interface IterableK<out T>
    where T : IterableK<T>
{
    /// <summary>
    /// Iterates from the first item in the structure to the last.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    static abstract Iterator<A> Forward<A>(K<T, A> ta);
}
