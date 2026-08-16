using LanguageExt.Traits;

public static class IndexableExtensions
{
    extension<T, KEY, VALUE>(K<T, VALUE> ta)
        where T : Tr.Indexable<T, KEY>
    {
        /// <summary>
        /// Find the element at the specified index or `None` if out of range
        /// </summary>
        /// <param name="index">Index value</param>
        /// <returns>Result at index if found, otherwise `None`</returns>
        public LE.Option<VALUE> At(KEY index) => 
            T.At(index, ta);
    }
}
