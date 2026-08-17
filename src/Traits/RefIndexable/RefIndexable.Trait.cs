using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

/// <summary>
/// Structure that supports element access by index.
/// </summary>
/// <remarks>
/// This is usually a hint that element access is fast and not dependent on the number of elements in the structure.
/// </remarks>
/// <typeparam name="T">Element value-type</typeparam>
/// <typeparam name="KEY">Index value-type</typeparam>
public interface RefIndexable<T, KEY>
    where T : RefIndexable<T, KEY>
{
    /// <summary>
    /// Find the element at the specified index or `Unsafe.NilRef`
    /// </summary>
    public static abstract ref readonly A AtRef<A>(in KEY index, in K<T, A> ta);
}
