using IteratorPrototype.Traits;

namespace IteratorPrototype;

public static partial class Prelude
{
    /// <summary>
    /// Fold the structure: `ta` and pass each element that it yields to `f`, resulting in an `F` applicative-value.
    /// The fold operator is applicative `Action`, which causes each applicative-value to be sequenced.      
    /// </summary>
    /// <param name="ta">Foldable structure</param>
    /// <param name="f">Mapping operation</param>
    /// <typeparam name="T">Foldable</typeparam>
    /// <typeparam name="F">Applicative</typeparam>
    /// <typeparam name="A">Input bound value</typeparam>
    /// <typeparam name="B">Mapping bound value</typeparam>
    /// <returns></returns>
    public static LT.K<F, LE.Unit> forM<T, F, A, B>(LT.K<T, A> ta, Func<A, LT.K<F, B>> f)
        where F : LT.Applicative<F>
        where T : Foldable<T> =>
        ta.ForM(f);
    
    /// <summary>
    /// Same behaviour as `Fold` but allows early exit of the operation once
    /// the predicate function becomes `false` for the state/value pair 
    /// </summary>
    public static S foldWhile<T, A, S>(
        Func<S, A, S> f, 
        Func<(S State, A Value), bool> predicate, 
        S initialState,
        LT.K<T, A> ta)
        where T : Foldable<T> =>
        ta.FoldWhile(f, predicate, initialState);

    /// <summary>
    /// Same behaviour as `Fold` but the fold operation returns a monadic type and allows
    /// early exit of the operation once the predicate function becomes `false` for the
    /// state/value pair 
    /// </summary>
    public static LT.K<M, S> foldWhileM<T, A, M, S>(
        Func<S, A, LT.K<M, S>> f, 
        Func<(S State, A Value), bool> predicate, 
        S initialState, 
        LT.K<T, A> ta) 
        where T : Foldable<T>
        where M : LT.Monad<M> =>
        ta.FoldWhileM(f, predicate, initialState);

    /// <summary>
    /// Same behaviour as `Fold` but allows early exit of the operation once
    /// the predicate function becomes `false` for the state/value pair
    /// </summary>
    public static S foldUntil<T, A, S>(
        Func<S, A, S> f,
        Func<(S State, A Value), bool> predicate,
        S initialState, 
        LT.K<T, A> ta) 
        where T : Foldable<T> =>
        ta.FoldUntil(f, predicate, initialState);

    /// <summary>
    /// Same behaviour as `Fold` but the fold operation returns a monadic type and allows
    /// early exit of the operation once the predicate function becomes `false` for the
    /// state/value pair 
    /// </summary>
    public static LT.K<M, S> foldUntilM<T, A, M, S>(
        Func<S, A, LT.K<M, S>> f, 
        Func<(S State, A Value), bool> predicate, 
        S initialState, 
        LT.K<T, A> ta) 
        where M : LT.Monad<M>
        where T : Foldable<T> => 
        ta.FoldUntilM(f, predicate, initialState);

    /// <summary>
    /// Right-associative fold of a structure, lazy in the accumulator.
    ///
    /// In the case of lists, 'Fold', when applied to a binary operator, a
    /// starting value (typically the right-identity of the operator), and a
    /// list, reduces the list using the binary operator, from right to left.
    /// </summary>
    public static S fold<T, A, S>(Func<S, A, S> f, S initialState, LT.K<T, A> ta) 
        where T : Foldable<T> =>
        ta.Fold(f, initialState);

    /// <summary>
    /// Right-associative fold of a structure, lazy in the accumulator.
    ///
    /// In the case of lists, 'Fold', when applied to a binary operator, a
    /// starting value (typically the right-identity of the operator), and a
    /// list, reduces the list using the binary operator, from right to left.
    /// </summary>
    public static LT.K<M, S> foldM<T, A, M, S>(
        Func<S, A, LT.K<M, S>> f, 
        S initialState, 
        LT.K<T, A> ta) 
        where T : Foldable<T>
        where M : LT.Monad<M> =>
        ta.FoldM(f, initialState);

    /// <summary>
    /// Given a structure with elements whose type is a `Monoid`, combine them
    /// via the monoid's `Append` operator.  This fold is right-associative and
    /// lazy in the accumulator.  When you need a strict left-associative fold,
    /// use 'foldMap'' instead, with 'id' as the map.
    /// </summary>
    public static A fold<T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where A : LT.Monoid<A> =>
        ta.Fold();

    /// <summary>
    /// Given a structure with elements whose type is a `Monoid`, combine them
    /// via the monoid's `Append` operator.  This fold is right-associative and
    /// lazy in the accumulator.  When you need a strict left-associative fold,
    /// use 'foldMap'' instead, with 'id' as the map.
    /// </summary>
    public static A foldWhile<T, A>(Func<(A State, A Value), bool> predicate, LT.K<T, A> ta) 
        where T : Foldable<T>
        where A : LT.Monoid<A> =>
        ta.FoldWhile(predicate) ;

    /// <summary>
    /// Given a structure with elements whose type is a `Monoid`, combine them
    /// via the monoid's `Append` operator.  This fold is right-associative and
    /// lazy in the accumulator.  When you need a strict left-associative fold,
    /// use 'foldMap'' instead, with 'id' as the map.
    /// </summary>
    public static A foldUntil<T, A>(Func<(A State, A Value), bool> predicate, LT.K<T, A> ta) 
        where T : Foldable<T>
        where A : LT.Monoid<A> =>
        ta.FoldUntil(predicate) ;

    /// <summary>
    /// Does an element that fits the predicate occur in the structure?
    /// </summary>
    public static bool exists<T, A>(Func<A, bool> predicate, LT.K<T, A> ta) 
        where T : Foldable<T> =>
        ta.Exists(predicate);

    /// <summary>
    /// Does the predicate hold for all elements in the structure?
    /// </summary>
    public static bool forAll<T, A>(Func<A, bool> predicate, LT.K<T, A> ta) 
        where T : Foldable<T> =>
        ta.ForAll(predicate);

    /// <summary>
    /// Map each element of a structure to an 'Applicative' action, evaluate these
    /// actions from left to right, and ignore the results.  For a version that
    /// doesn't ignore the results see `Traversable.traverse`.
    /// </summary>
    public static LT.K<F, LE.Unit> iterM<T, A, F, B>(Func<A, LT.K<F, B>> f, LT.K<T, A> ta)
        where T : Foldable<T>
        where F : LT.Monad<F> =>
        ta.IterM(f);
    
    /// <summary>
    /// Map each element of a structure to an action, evaluate these
    /// actions from left to right, and ignore the results.  For a version that
    /// doesn't ignore the results see `Traversable.traverse`.
    /// </summary>
    public static LE.Unit iter<T, A>(Action<int, A> f, LT.K<T, A> ta) 
        where T : Foldable<T> =>
        ta.Iter(f);
    
    /// <summary>
    /// Map each element of a structure to an action, evaluate these
    /// actions from left to right, and ignore the results.  For a version that
    /// doesn't ignore the results see `Traversable.traverse`.
    /// </summary>
    public static LE.Unit iter<T, A>(Action<A> f, LT.K<T, A> ta)
        where T : Foldable<T> =>
        ta.Iter(f);

    /// <summary>
    /// Partition a foldable into two sequences based on a predicate
    /// </summary>
    /// <param name="f">Predicate function</param>
    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <typeparam name="T">Foldable trait</typeparam>
    /// <returns>Partitioned structure</returns>
    public static (Arr<A> True, Arr<A> False) partition<T, A>(Func<A, bool> f, LT.K<T, A> ta)
        where T : Foldable<T> =>
        ta.Partition(f);
}
