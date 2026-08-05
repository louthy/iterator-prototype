namespace IteratorTest.Traits;

public static partial class Iterable
{
    /// <summary>
    /// Iterates from the first item in the structure to the last.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="TA">Iterable type</typeparam>
    /// <typeparam name="IA">Iterator type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static IA forward<TA, IA, A>(in TA ta)
        where TA : Iterable<TA, IA, A> 
        where IA : IIterator<IA, A> =>
        TA.Forward(ta);
    
    /// <summary>
    /// Iterates from the first item in the structure to the last.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <typeparam name="TA">Iterable type</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    /// <returns>Iterator</returns>
    public static Iterator<A> forward<TA, A>(in TA ta)
        where TA : Iterable<TA, Iterator<A>, A> => 
        TA.Forward(ta);
}