using IteratorTest.Traits;

namespace IteratorTest;

public static partial class Iterator
{
    /// <summary>
    /// Construct a new iterator that yields a single value.
    /// </summary>
    /// <param name="head">Singleton value</param>
    /// <typeparam name="TA">Iterable type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<TA, IS, A> singleton<TA, IS, A>(in A head) 
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        new (in head);
    
    /// <summary>
    /// Construct a new iterator from a head value and the tail remainder.
    /// </summary>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail remainder</param>
    /// <typeparam name="TA">Iterable type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<TA, IS, A> cons<TA, IS, A>(in A head, Iterator<TA, IS, A> tail) 
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        new (in head, tail);    
    
    /// <summary>
    /// Construct a new iterator from a head value and a (lazily acquired) tail remainder.
    /// </summary>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail remainder</param>
    /// <typeparam name="TA">Iterable type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<TA, IS, A> cons<TA, IS, A>(in A head, Func<Iterator<TA, IS, A>> tail) 
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        new (in head, tail);    
    
    /// <summary>
    /// Construct a new iterator from an initial selection of items and a last singleton item.
    /// </summary>
    /// <param name="init">Initial items</param>
    /// <param name="last">Last item</param>
    /// <typeparam name="TA">Iterable type</typeparam>
    /// <typeparam name="IS">Iterator state</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<TA, IS, A> add<TA, IS, A>(Iterator<TA, IS, A> init, in A last) 
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        new (init, in last);    
    
    /// <summary>
    /// Construct a new iterator from a function that yields an iterator when invoked.
    /// </summary>
    /// <remarks>
    /// This delays processing until enumeration of the values is started.
    /// </remarks>
    /// <param name="iterator">Lazily acquired iterator</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<TA, IS, A> lazy<TA, IS, A>(Func<Iterator<TA, IS, A>> iterator) 
        where TA : class, IterableImmutable<TA, IS, A>
        where IS : struct =>
        new (iterator);
}