using IteratorPrototype.Traits;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

public static class IterableImmutableExtensions
{
    extension<T, IS, A>(K<T, A> ta)
        where T : IterableImmutable<T, IS>
        where IS : unmanaged
    {
        /// <summary>
        /// Get an enumerator for the immutable-iterable in the inheritance chain
        /// </summary>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public IterableImmutableEnumerator<T, IS, A> GetEnumerator() =>
            new(ta);

        /// <summary>
        /// Perform an action on each element of the iterable
        /// </summary>
        /// <param name="f">Action to perform</param>
        /// <returns>The original unchanged structure</returns>
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public K<T, A> Do(Action<A> f) =>
            T.Do(ta, f);

        /// <summary>
        /// Set up the mutable state for use with `StepMutable`.
        /// </summary>
        /// <returns>Iterable state</returns>
        [Pure]
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public IS SetupImmutable() =>
            T.SetupImmutable(ta);

        /// <summary>
        /// Set up the mutable state for use with `StepMutable`.
        /// </summary>
        /// <returns>Iterable state</returns>
        [MethodImpl(Root.Optimisations.InliningOnly)]
        public bool StepImmutable(in IS ts, out A head, out IS tail) =>
            T.StepImmutable(ta, in ts, out head, out tail);
    }
}