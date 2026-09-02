using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;
using LanguageExt.Traits;

public static partial class IterableExtensions
{
    extension<T, A>(K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Get the forward iterator
        /// </summary>
        /// <returns>An iterator that enumerates from the 'first' element to the 'last' element.</returns>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public Root.Iterator<A> Forward() =>
            T.Forward(ta);

        /// <summary>
        /// Bounce the iterable to a span
        /// </summary>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public ReadOnlySpan<A> AsSpan() =>
            T.AsSpan(ta);

        /// <summary>
        /// Bounce the iterable to an array
        /// </summary>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public A[] ToArray() =>
            [.. T.AsSpan(ta)];

        /// <summary>
        /// Write every element of this iterable to the `ArrayWriter` provided
        /// </summary>
        /// <param name="writer">Writer to emit the elements to</param>
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public LE.Unit ToWriter(ref LE.ArrayWriter<A> writer) =>
            T.ToWriter(ta, ref writer);

        /// <summary>
        /// Get an enumerable from the iterable 
        /// </summary>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public IEnumerable<A> AsEnumerable() =>
            T.AsEnumerable(ta);

        /// <summary>
        /// Convert to a queryable 
        /// </summary>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public IQueryable<A> AsQueryable() =>
            // NOTE TO FUTURE ME: Don't delete this thinking it isn't required!
            // NOTE FROM FUTURE ME: Next time you leave a message for your future self, explain your reasoning.
            T.AsEnumerable(ta).AsQueryable();    

        /// <summary>
        /// Get an enumerator for the iterable 
        /// </summary>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public IterableEnumerator<T, A> GetEnumerator() =>
            T.GetEnumerator(ta);

        /// <summary>
        /// Show up to 50 items in string form, separated by the separator argument or a comma if no
        /// separator is provided.
        /// </summary>
        /// <remarks>
        /// Use `ToFullString` if you want to see all items in the iterable.
        /// </remarks>
        /// <param name="separator">Characters to separate each element by</param>
        /// <returns>A constructed string of up to 50 items</returns>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public string ToString(string separator = ", ") =>
            T.ToString(ta, separator);

        /// <summary>
        /// Show up to 50 items in string form, separated by the separator argument or a comma if no
        /// separator is provided.  The string will be enclosed in square brackets.
        /// </summary>
        /// <remarks>
        /// Use `ToFullArrayString` if you want to see all items in the iterable.
        /// </remarks>
        /// <param name="separator">Characters to separate each element by</param>
        /// <returns>A constructed string of up to 50 items</returns>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public string ToArrayString(string separator = ", ") =>
            T.ToArrayString(ta, separator);

        /// <summary>
        /// Show all elements from the iterable in string form, separated by the separator argument or a comma if no
        /// separator is provided.
        /// </summary>
        /// <remarks>
        /// Use `ToString` if you want to limit the number of items shown to 50.
        /// </remarks>
        /// <param name="separator">Characters to separate each element by</param>
        /// <returns>A constructed string of all elements</returns>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public string ToFullString(string separator = ", ") =>
            T.ToFullString(ta, separator);

        /// <summary>
        /// Show all elements from the iterable in string form, separated by the separator argument or a comma if no
        /// separator is provided.  The string will be enclosed in square brackets.
        /// </summary>
        /// <remarks>
        /// Use `ToString` if you want to limit the number of items shown to 50.
        /// </remarks>
        /// <param name="separator">Characters to separate each element by</param>
        /// <returns>A constructed string of all elements</returns>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public string ToFullArrayString(string separator = ", ") =>
            T.ToFullArrayString(ta, separator);
    }
}