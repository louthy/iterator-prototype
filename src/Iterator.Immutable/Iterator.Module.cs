using IteratorPrototype.Traits;

namespace IteratorPrototype;

public partial class Iterator
{
    /// <summary>
    /// Construct a new iterator that yields a single value.
    /// </summary>
    /// <param name="head">Singleton value</param>
    /// <typeparam name="T">Iterable trait type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<T, IS, A> singleton<T, IS, A>(in A head) 
        where T : class, IterableImmutable<T, IS>
        where IS : struct =>
        new (in head);
    
    /// <summary>
    /// Construct a new iterator from a head value and the tail remainder.
    /// </summary>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail remainder</param>
    /// <typeparam name="T">Iterable trait type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<T, IS, A> cons<T, IS, A>(in A head, Iterator<T, IS, A> tail) 
        where T : class, IterableImmutable<T, IS>
        where IS : struct =>
        new (in head, tail);    
    
    /// <summary>
    /// Construct a new iterator from a head value and a (lazily acquired) tail remainder.
    /// </summary>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail remainder</param>
    /// <typeparam name="T">Iterable trait type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<T, IS, A> cons<T, IS, A>(in A head, Func<Iterator<T, IS, A>> tail) 
        where T : class, IterableImmutable<T, IS>
        where IS : struct =>
        new (in head, tail);    
    
    /// <summary>
    /// Construct a new iterator from an initial selection of items and a last singleton item.
    /// </summary>
    /// <param name="init">Initial items</param>
    /// <param name="last">Last item</param>
    /// <typeparam name="T">Iterable trait type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<T, IS, A> add<T, IS, A>(Iterator<T, IS, A> init, in A last) 
        where T : class, IterableImmutable<T, IS>
        where IS : struct =>
        new (init, in last);    
    
    /// <summary>
    /// Construct a new iterator from a function that yields an iterator when invoked.
    /// </summary>
    /// <remarks>
    /// This delays processing until enumeration of the values is started.
    /// </remarks>
    /// <param name="iterator">Lazily acquired iterator</param>
    /// <typeparam name="T">Iterable trait type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<T, IS, A> lazy<T, IS, A>(Func<Iterator<T, IS, A>> iterator) 
        where T : class, IterableImmutable<T, IS>
        where IS : struct =>
        new (iterator);
}