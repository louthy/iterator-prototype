using LanguageExt.Traits;

namespace IteratorTest.Traits;

/// <summary>
/// A specialised version of <see cref="IterableK{T, IS}"/> that allows fast enumeration using a mutable `ref struct`
/// state.
/// </summary>
/// <typeparam name="T">Trait type</typeparam>
/// <typeparam name="IS">Immutable state type</typeparam>
/// <typeparam name="MS">Mutable state type</typeparam>
public interface IterableK<T, IS, MS> : IterableK<T, IS>
    where T : IterableK<T, IS, MS>
    where IS : struct
    where MS : allows ref struct
{
    static abstract void SetupMutable<A>(K<T, A> ta, out MS state);
    
    /// <summary>
    /// Used for high-performance, mutable, iteration.
    /// </summary>
    static abstract bool StepMutable<A>(K<T, A> ta, ref MS ts, out A value);
}
