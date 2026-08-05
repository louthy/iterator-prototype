namespace IteratorTest;

public static partial class Iterator
{
    /// <summary>
    /// Construct a new iterator that yields no values.
    /// </summary>
    /// <remarks>
    /// This returns `Nil` so it can be coerced using implicit conversion operators to
    /// `Iterator{TA, IS, A}` and `Iterator{A}`.
    /// </remarks>
    public static Nil empty =>
        default;
    
    /// <summary>
    /// Construct a new iterator that yields a single value.
    /// </summary>
    /// <param name="head">Singleton value</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<A> singleton<A>(in A head) =>
        new (in head);
    
    /// <summary>
    /// Construct a new iterator from a head value and the tail remainder.
    /// </summary>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail remainder</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns></returns>
    public static Iterator<A> cons<A>(in A head, Iterator<A> tail) =>
        new (in head, tail);
    
    /// <summary>
    /// Construct a new iterator from a head value and a (lazily acquired) tail remainder.
    /// </summary>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail remainder</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns></returns>
    public static Iterator<A> cons<A>(in A head, Func<Iterator<A>> tail) =>
        new (in head, tail);
    
    /// <summary>
    /// Construct a new iterator from an initial selection of items and a last singleton item.
    /// </summary>
    /// <param name="init">Initial items</param>
    /// <param name="last">Last item</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<A> add<A>(Iterator<A> init, in A last) => 
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
    public static Iterator<A> lazy<A>(Func<Iterator<A>> iterator) =>
        new (iterator);
}