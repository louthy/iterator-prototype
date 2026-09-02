using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Traits;

public static class Iterable
{
    /// <summary>
    /// Iterates from the first item in the structure to the last.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iterator<A> forward<T, A>(K<T, A> ta)
        where T : Iterable<T> =>
        ta.Forward();

    /// <summary>
    /// Bounce the iterable to a span
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static ReadOnlySpan<A> asSpan<T, A>(K<T, A> ta) 
        where T : Iterable<T> =>
        T.AsSpan(ta);

    /// <summary>
    /// Bounce the iterable to an array
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static A[] toArray<T, A>(K<T, A> ta) 
        where T : Iterable<T> =>
        [.. T.AsSpan(ta)];

    /// <summary>
    /// Write every element of this iterable to the `ArrayWriter` provided
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <param name="writer">Writer to emit the elements to</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    [MethodImpl(Optimisations.InliningOnly)]
    public static LE.Unit toWriter<T, A>(K<T, A> ta, ref LE.ArrayWriter<A> writer) 
        where T : Iterable<T> =>
        T.ToWriter(ta, ref writer);

    /// <summary>
    /// Get an enumerable from the iterable 
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static IEnumerable<A> asEnumerable<T, A>(K<T, A> ta) 
        where T : Iterable<T> =>
        T.AsEnumerable(ta);

    /// <summary>
    /// Get an enumerator for the iterable 
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static IterableEnumerator<T, A> getEnumerator<T, A>(K<T, A> ta) 
        where T : Iterable<T> =>
        T.GetEnumerator(ta);

    /// <summary>
    /// Show up to 50 items in string form, separated by the separator argument or a comma if no
    /// separator is provided.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <param name="separator">Characters to separate each element by</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <remarks>
    /// Use `ToFullString` if you want to see all items in the iterable.
    /// </remarks>
    /// <returns>A constructed string of up to 50 items</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static string toString<T, A>(K<T, A> ta, string separator = ", ") 
        where T : Iterable<T> =>
        T.ToString(ta, separator);

    /// <summary>
    /// Show up to 50 items in string form, separated by the separator argument or a comma if no
    /// separator is provided.  The string will be enclosed in square brackets.
    /// </summary>
    /// <remarks>
    /// Use `ToFullArrayString` if you want to see all items in the iterable.
    /// </remarks>
    /// <param name="ta">Structure to iterate</param>
    /// <param name="separator">Characters to separate each element by</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>A constructed string of up to 50 items</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static string toArrayString<T, A>(K<T, A> ta, string separator = ", ") 
        where T : Iterable<T> =>
        T.ToArrayString(ta, separator);

    /// <summary>
    /// Show all elements from the iterable in string form, separated by the separator argument or a comma if no
    /// separator is provided.
    /// </summary>
    /// <remarks>
    /// Use `ToString` if you want to limit the number of items shown to 50.
    /// </remarks>
    /// <param name="ta">Structure to iterate</param>
    /// <param name="separator">Characters to separate each element by</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>A constructed string of all elements</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static string toFullString<T, A>(K<T, A> ta, string separator = ", ") 
        where T : Iterable<T> =>
        T.ToFullString(ta, separator);

    /// <summary>
    /// Show all elements from the iterable in string form, separated by the separator argument or a comma if no
    /// separator is provided.  The string will be enclosed in square brackets.
    /// </summary>
    /// <remarks>
    /// Use `ToString` if you want to limit the number of items shown to 50.
    /// </remarks>
    /// <param name="ta">Structure to iterate</param>
    /// <param name="separator">Characters to separate each element by</param>
    /// <typeparam name="T">Trait type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>A constructed string of all elements</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    public static string toFullArrayString<T, A>(K<T, A> ta, string separator = ", ") 
        where T : Iterable<T> =>
        T.ToFullArrayString(ta, separator);
}