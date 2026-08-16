using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

public static class Indexable
{
    /// <summary>
    /// Find the element at the specified index or `None` if out of range
    /// </summary>
    /// <param name="index">Index value</param>
    /// <returns>Result at index if found, otherwise `None`</returns>
    public static LE.Option<VALUE> at<T, KEY, VALUE>(KEY index, K<T, VALUE> ta) 
        where T : Indexable<T, KEY> => 
        T.At(index, ta);
}
