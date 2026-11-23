using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

/// <summary>
/// Used by sequence types 
/// </summary>
/// <typeparam name="A">Element type</typeparam>
public union Head<A>(Cons<A>, Nil);

/// <summary>
/// Used by sequence types 
/// </summary>
/// <typeparam name="T">Iterable trait type</typeparam>
/// <typeparam name="A">Element type</typeparam>
public union Head<T, A>(Cons<T, A>, Nil);

/// <summary>
/// Nil unit type
/// </summary>
public readonly record struct Nil
{
    /// <summary>
    /// Cached instance for convenience
    /// </summary>
    public static readonly Nil Default = new Nil();
    
    /// <summary>
    /// Cached instance for convenience
    /// </summary>
    public static readonly object DefaultObj = new Nil();
}

/// <summary>
/// A list constructed from a head value and a tail sequence
/// </summary>
/// <param name="Tail">Tail sequence</param>
/// <typeparam name="A">Element type</typeparam>
public readonly record struct Cons<A>(in A Head, in Iter<A> Tail);  

/// <summary>
/// A list constructed from a head value and a tail sequence
/// </summary>
/// <param name="Head">Head value</param>
/// <param name="Tail">Tail sequence</param>
/// <typeparam name="T">Iterable trait type</typeparam>
/// <typeparam name="A">Element type</typeparam>
public readonly record struct Cons<T, A>(in A Head, K<T, A> Tail);  