using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

public static class IterableMutableExtensions
{
    extension<T, IS, MS, A>(K<T, A> ta)
        where T : IterableMutable<T, IS, MS>
        where IS : unmanaged
        where MS : allows ref struct
    {
        /// <summary>
        /// Get an enumerator for the iterable
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IterableMutableEnumerator<T, IS, MS, A> GetEnumerator() =>
            new(ta);

        /// <summary>
        /// Perform an action on each element of the iterable
        /// </summary>
        /// <param name="f">Action to perform</param>
        /// <returns>The original unchanged structure</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public K<T, A> Do(Action<A> f) =>
            T.Do(ta, f);

        /// <summary>
        /// Set up the mutable state for use with `StepMutable`.
        /// </summary>
        /// <returns>Iterable state</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MS SetupMutable() =>
            T.SetupMutable(ta);

        /// <summary>
        /// Set up the mutable state for use with `StepMutable`.
        /// </summary>
        /// <returns>Iterable state</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool StepMutable(ref MS ts, out A value) =>
            T.StepMutable(ta, ref ts, out value);
    }
}