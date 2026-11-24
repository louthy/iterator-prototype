using IteratorPrototype.Iterator3;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class IterableExtensions
{
    extension<T, A>(LT.K<T, A> ta)
        where T : Iterable<T>
    {
        /// <summary>
        /// Implementation of LINQ `Select` operator for all `Iterables`. Instead of
        /// projecting to the same type (like `Functor.Map`), this projects to `Iter`
        /// allowing continued lazy processing (like `IEnumerable` in dotnet, but without
        /// mutable enumerators). 
        /// </summary>
        /// <param name="f">Mapping function</param>
        /// <returns>Lazy iterator</returns>
        public Iter<B> Select<B>(Func<A, B> f) =>
            T.Select(f, ta);
        
        /// <summary>
        /// Implementation of LINQ `SelectMany` operator for all `Iterables`. Instead of
        /// projecting to the same type (like `Monad.Bind`), this projects to `Iter`
        /// allowing continued lazy processing (like `IEnumerable` in dotnet, but without
        /// mutable enumerators). 
        /// </summary>
        /// <param name="f">Bind function</param>
        /// <returns>Lazy iterator</returns>
        public Iter<B> SelectMany<B>(Func<A, Iter<B>> f) =>
            T.SelectMany(f, ta);
        
        /// <summary>
        /// Implementation of LINQ `SelectMany` operator for all `Iterables`. Instead of
        /// projecting to the same type (like `Monad.Bind`), this projects to `Iter`
        /// allowing continued lazy processing (like `IEnumerable` in dotnet, but without
        /// mutable enumerators). 
        /// </summary>
        /// <param name="bind">Bind function</param>
        /// <param name="project">Project function</param>
        /// <returns>Lazy iterator</returns>
        public Iter<C> SelectMany<B, C>(Func<A, Iter<B>> bind, Func<A, B, C> project) =>
            T.SelectMany(bind, project, ta);

        /// <summary>
        /// Implementation of LINQ `Where` operator for all `Iterables`. Instead of
        /// projecting to the same type (like `MonoidK.Filter`), this projects to `Iter`
        /// allowing continued lazy processing (like `IEnumerable` in dotnet, but without
        /// mutable enumerators). 
        /// </summary>
        /// <param name="f">Bind function</param>
        /// <returns>Lazy iterator</returns>
        public Iter<A> Where(Func<A, bool> f) =>
            T.Where(f, ta);
        
        /// <summary>
        /// Applies an accumulator function over a range.
        /// </summary>
        public S Aggregate<S>(S state, Func<S, A, S> folder) =>
            T.Aggregate(state, folder, ta);

        /// <summary>
        /// Determines whether any element of a range satisfies a condition.
        /// </summary>
        public bool Any(Func<A, bool> predicate) =>
            T.Any(predicate, ta);

        /// <summary>
        /// Determines whether all elements of a range satisfy a condition.
        /// </summary>
        public bool All(Func<A, bool> predicate) =>
            T.All(predicate, ta);
        
        /// <summary>
        /// Filter out items that don't match the specified type.
        /// </summary>
        public Iter<B> Cast<B>() =>
            T.Cast<A, B>(ta);

        /// <summary>
        /// Reverse the order of the elements in a range.
        /// </summary>
        public Root.Arr<A> Reverse() =>
            T.Reverse(ta);

    }
}