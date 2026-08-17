using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

public static class RefIndexable
{
    /// <summary>
    /// Find the element at the specified index or `Unsafe.NilRef`
    /// </summary>
    /// <param name="index">Index value</param>
    /// <returns>Result at index if found, otherwise `Unsafe.NilRef`</returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static ref readonly VALUE at<T, KEY, VALUE>(in KEY index, in K<T, VALUE> ta) 
        where T : RefIndexable<T, KEY> => 
        ref T.AtRef(index, ta);
}
