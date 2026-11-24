using System.Numerics;
using IteratorPrototype;
using IteratorPrototype.Traits;
using IteratorPrototype.Iterator3;
using static LanguageExt.Prelude;
using LanguageExt.ClassInstances;

public static partial class FoldableExtensions
{
    /// <param name="f">Mapping operation</param>
    /// <typeparam name="T">Foldable</typeparam>
    /// <typeparam name="F">Applicative</typeparam>
    /// <typeparam name="A">Input bound value</typeparam>
    /// <typeparam name="B">Mapping bound value</typeparam>
    extension<T, F, A, B>(Func<A, LT.K<F, B>> f)
        where T : Foldable<T>
        where F : LT.Applicative<F>
    {
        /// <summary>
        /// Fold the structure: `ta` and pass each element that it yields to `f`, resulting in an `F` applicative-value.
        /// The fold operator is applicative `Action`, which causes each applicative-value to be sequenced.      
        /// </summary>
        /// <param name="ta">Foldable structure</param>
        /// <returns></returns>
        public LT.K<F, LE.Unit> ForM(LT.K<T, A> ta) =>
            ta.Fold((fs, x) =>  F.BackAction(fs, f(x)), pure<F, LE.Unit>(unit));
    }

    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="T">Foldable</typeparam>
    /// <typeparam name="F">Applicative</typeparam>
    /// <typeparam name="A">Input bound value</typeparam>
    /// <typeparam name="B">Mapping bound value</typeparam>
    extension<T, F, A, B>(LT.K<T, A> ta)
        where T : Foldable<T>
        where F : LT.Applicative<F>
    {
        /// <summary>
        /// Fold the structure: `ta` and pass each element that it yields to `f`, resulting in an `F` applicative-value.
        /// The fold operator is applicative `Action`, which causes each applicative-value to be sequenced.      
        /// </summary>
        /// <param name="f">Mapping operation</param>
        /// <returns></returns>
        public LT.K<F, LE.Unit> ForM(Func<A, LT.K<F, B>> f) =>
            T.Fold((fs, x) => F.BackAction(fs, f(x)), pure<F, LE.Unit>(unit), ta);
    }

    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="T">Foldable</typeparam>
    /// <typeparam name="A">Value type</typeparam>
    extension<T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
    {
        /// <summary>
        /// Fold until the `Option` returns `None`
        /// </summary>
        /// <param name="f">Fold function</param>
        /// <param name="initialState">Initial state for the fold</param>
        /// <typeparam name="S">State type</typeparam>
        /// <returns>Aggregated value</returns>
        public S FoldMaybe<S>(
            Func<S, A, LE.Option<S>> f,
            S initialState) =>
            T.FoldMaybe(f, initialState, ta);

        /// <summary>
        /// Same behaviour as `Fold` but allows early exit of the operation once
        /// the predicate function becomes `false` for the state/value pair 
        /// </summary>
        public S FoldWhile<S>(
            Func<S, A, S> f,
            Func<(S State, A Value), bool> predicate,
            S initialState) =>
            T.FoldWhile(f, predicate, initialState, ta);

        /// <summary>
        /// Same behaviour as `Fold` but the fold operation returns a monadic type and allows
        /// early exit of the operation once the predicate function becomes `false` for the
        /// state/value pair 
        /// </summary>
        public LT.K<M, S> FoldWhileM<M, S>(
            Func<S, A, LT.K<M, S>> f,
            Func<(S State, A Value), bool> predicate,
            S initialState)
            where M : LT.Monad<M> =>
            T.FoldWhileM<LT.K<M, S>, M, A, S>(f, predicate, initialState, ta);

        /// <summary>
        /// Same behaviour as `Fold` but allows early exit of the operation once
        /// the predicate function becomes `false` for the state/value pair
        /// </summary>
        public S FoldUntil<S>(
            Func<S, A, S> f,
            Func<(S State, A Value), bool> predicate,
            S initialState) =>
            T.FoldUntil(f, predicate, initialState, ta);

        /// <summary>
        /// Same behaviour as `Fold` but the fold operation returns a monadic type and allows
        /// early exit of the operation once the predicate function becomes `false` for the
        /// state/value pair 
        /// </summary>
        public LT.K<M, S> FoldUntilM<M, S>(
            Func<S, A, LT.K<M, S>> f,
            Func<(S State, A Value), bool> predicate,
            S initialState)
            where M : LT.Monad<M> =>
            T.FoldUntilM<LT.K<M, S>, M, A, S>(f, predicate, initialState, ta);

        /// <summary>
        /// Right-associative fold of a structure, lazy in the accumulator.
        ///
        /// In the case of lists, 'Fold', when applied to a binary operator, a
        /// starting value (typically the right-identity of the operator), and a
        /// list, reduces the list using the binary operator, from right to left.
        /// </summary>
        public S Fold<S>(Func<S, A, S> f, S initialState) =>
            T.Fold(f, initialState, ta);

        /// <summary>
        /// Right-associative fold of a structure, lazy in the accumulator.
        ///
        /// In the case of lists, 'Fold', when applied to a binary operator, a
        /// starting value (typically the right-identity of the operator), and a
        /// list, reduces the list using the binary operator, from right to left.
        /// </summary>
        public LT.K<M, S> FoldM<M, S>(Func<S, A, LT.K<M, S>> f, S initialState)
            where M : LT.Monad<M> =>
            T.FoldM<LT.K<M, S>, M, A, S>(f, initialState, ta);

        /// <summary>
        /// List of elements of a structure, from left to right
        /// </summary>
        public Seq<A> ToSeq() =>
            T.ToSeq(ta);

        /// <summary>
        /// List of elements of a structure, from left to right
        /// </summary>
        public Lst<A> ToLst() =>
            T.ToLst(ta);

        /// <summary>
        /// List of elements of a structure, from left to right
        /// </summary>
        public Arr<A> ToArr() =>
            T.ToArr(ta);

        /// <summary>
        /// List of elements of a structure, from left to right
        /// </summary>
        public bool IsEmpty =>
            T.IsEmpty(ta);

        /// <summary>
        /// Does an element that fits the predicate occur in the structure?
        /// </summary>
        public bool Exists(Func<A, bool> predicate) =>
            T.Exists(predicate, ta);

        /// <summary>
        /// Does the predicate hold for all elements in the structure?
        /// </summary>
        public bool ForAll(Func<A, bool> predicate) =>
            T.ForAll(predicate, ta);

        /// <summary>
        /// Does the element exist in the structure?
        /// </summary>
        public bool Contains(A value) =>
            T.Contains(value, ta);

        /// <summary>
        /// Find the first element that matches the predicate
        /// </summary>
        public LE.Option<A> Find(int startIndex, int count, Func<A, bool> predicate) =>
            T.Find(startIndex, count, predicate, ta);
    
        /// <summary>
        /// Find the first element that matches the predicate
        /// </summary>
        public LE.Option<A> Find(int startIndex, Func<A, bool> predicate) =>
            T.Find(startIndex, None, predicate, ta);
    
        /// <summary>
        /// Find the first element that matches the predicate
        /// </summary>
        public LE.Option<A> Find(Func<A, bool> predicate) =>
            T.Find(None, None, predicate, ta);

        /// <summary>
        /// Get the head item in the foldable or `None`
        /// </summary>
        public LE.Option<A> Head =>
            T.Head(ta);
        
        /// <summary>
        /// Iterate over the structure from left to right, applying the monadic action to each element.
        /// </summary>
        public LT.K<F, LE.Unit> IterM<F, B>(Func<A, LT.K<F, B>> f)
            where F : LT.Monad<F> =>
            T.IterM<LT.K<F, B>, F, A, B>(f, ta);

        /// <summary>
        /// Iterate over the structure from left to right, applying the action to each element.
        /// </summary>
        public LE.Unit Iter(Action<A> f) =>
            T.Iter(f, ta);

        /// <summary>
        /// Iterate over the structure from left to right, applying the action to each element.
        /// </summary>
        public LE.Unit Iter(Action<int, A> f, int initialIndex = 0) =>
            T.Iter(f, initialIndex, ta);

        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public LE.Option<A> Min() =>
            T.Min(ta);

        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public LE.Option<A> Max() =>
            T.Max(ta);

        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public A Min(A initialMin) =>
            T.Min(initialMin, ta);

        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public A Max(A initialMax) =>
            T.Max(initialMax, ta);

        /// <summary>
        /// Find the first index of an element in the structure that matches the predicate
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="count">Maximum number of elements to test before giving up</param>
        /// <param name="predicate">When this predicate returns true, the current index is returned.</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, LE.Option<int> count, Func<A, bool> predicate) =>
            T.IndexOf(startIndex, count, predicate, ta);

        /// <summary>
        /// Find the first index of an element in the structure that matches the predicate
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="predicate">When this predicate returns true, the current index is returned.</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, Func<A, bool> predicate) =>
            T.IndexOf(startIndex, None, predicate, ta);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the predicate
        /// </summary>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(Func<A, bool> predicate) =>
            T.IndexOf(None, None, predicate, ta);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="count">Maximum number of elements to test before giving up</param>
        /// <param name="item">Element to search for</param>
        /// <param name="eq">Equality comparer</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, LE.Option<int> count, A item, IEqualityComparer<A> eq) =>
            T.IndexOf(startIndex, count, x => eq.Equals(item, x), ta);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="item">Element to search for</param>
        /// <param name="eq">Equality comparer</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, A item, IEqualityComparer<A> eq) => 
            T.IndexOf(startIndex, None, x => eq.Equals(item, x), ta);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="item">Element to search for</param>
        /// <param name="eq">Equality comparer</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(A item, IEqualityComparer<A> eq) => 
            T.IndexOf(None, None, x => eq.Equals(item, x), ta);

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="count">Maximum number of elements to test before giving up</param>
        /// <param name="item">Element to search for</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, LE.Option<int> count, A item) =>
            ta.IndexOf<EqDefault<A>, T, A>(startIndex, count, item);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="item">Element to search for</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, A item) => 
            ta.IndexOf<EqDefault<A>, T, A>(startIndex, item);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="item">Element to search for</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(A item) => 
            ta.IndexOf<EqDefault<A>, T, A>(item);    

        /// <summary>
        /// Partition foldable into two sequences based on a predicate
        /// </summary>
        /// <param name="f">Predicate function</param>
        /// <typeparam name="A">Bound value type</typeparam>
        /// <returns>Partitioned structure</returns>
        public (Arr<A> True, Arr<A> False) Partition(Func<A, bool> f) =>
            T.Partition(f, ta);
    
        /// <summary>
        /// Inject a value in between each item in the enumerable 
        /// </summary>
        /// <param name="sep">Item to inject</param>
        /// <returns>An iterable with the values injected</returns>
        public Iter<A> Intersperse(A sep) =>
            T.Intersperse(sep, ta);
    }

    extension<T, M, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where M : LT.Alternative<M>
    {
        /// <summary>
        /// Get the head item in the `FoldableIO` or `Alternative.Empty`
        /// </summary>
        public LT.K<M, A> HeadM() =>
            T.HeadM<M, A>(ta);
    }

    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <typeparam name="EqA">Equality trait</typeparam>
    /// <typeparam name="T">Foldable trait</typeparam>
    extension<EqA, T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where EqA : LT.Eq<A> 
    {
        /// <summary>
        /// Does the element exist in the structure?
        /// </summary>
        public bool Contains(A value) =>
            T.Contains<EqA, A>(value, ta);

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="count">Maximum number of elements to test before giving up</param>
        /// <param name="item">Element to search for</param>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, LE.Option<int> count, A item) =>
            T.IndexOf(startIndex, count, x => EqA.Equals(item, x), ta);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="startIndex">Initial index to start the search</param>
        /// <param name="item">Element to search for</param>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(LE.Option<int> startIndex, A item) =>
            T.IndexOf(startIndex, None, x => EqA.Equals(item, x), ta);    

        /// <summary>
        /// Find the first index of an element in the structure that matches the element provided
        /// </summary>
        /// <param name="item">Element to search for</param>
        /// <returns>`Some(index)` if the predicate returns `true`, otherwise `None`</returns>
        public LE.Option<int> IndexOf(A item) => 
            T.IndexOf(None, None, x => EqA.Equals(item, x), ta);    
    }

    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="A">Value type</typeparam>
    /// <typeparam name="OrdA">Ordering trait</typeparam>
    /// <typeparam name="T">Foldable trait</typeparam>
    extension<OrdA, T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where OrdA : LT.Ord<A>
    {
        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public LE.Option<A> Min() =>
            T.Min<OrdA, A>(ta);

        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public LE.Option<A> Max() =>
            T.Max<OrdA, A>(ta);

        /// <summary>
        /// Find the minimum value in the structure
        /// </summary>
        public A Min(A initialMin) =>
            T.Min<OrdA, A>(initialMin, ta);

        /// <summary>
        /// Find the maximum value in the structure
        /// </summary>
        public A Max(A initialMax) =>
            T.Max<OrdA, A>(initialMax, ta);
    }
    
    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="T">Foldable type</typeparam>
    /// <typeparam name="A">Bound values</typeparam>
    extension<T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where A : LT.Monoid<A>
    {
        /// <summary>
        /// Given a structure with elements whose type is a `Monoid`, combine them
        /// via the monoid's `Append` operator.  This fold is right-associative and
        /// lazy in the accumulator.  When you need a strict left-associative fold,
        /// use `FoldMap` instead, with `identity` as the map.
        /// </summary>
        public A Fold() =>
            T.Fold((s, x) => s + x, A.Empty, ta);

        /// <summary>
        /// Given a structure with elements whose type is a `Monoid`, combine them
        /// via the monoid's `Append` operator.  This fold is right-associative and
        /// lazy in the accumulator.  When you need a strict left-associative fold,
        /// use `FoldMap` instead, with `identity` as the map.
        /// </summary>
        public A FoldWhile(Func<(A State, A Value), bool> predicate) =>
            T.FoldWhile((s, x) => s + x, predicate, A.Empty, ta);

        /// <summary>
        /// Given a structure with elements whose type is a `Monoid`, combine them
        /// via the monoid's `Append` operator.  This fold is right-associative and
        /// lazy in the accumulator.  When you need a strict left-associative fold,
        /// use `FoldMap` instead, with `identity` as the map.
        /// </summary>
        public A FoldUntil(Func<(A State, A Value), bool> predicate) =>
            T.FoldUntil((s, x) => s + x, predicate, A.Empty, ta);
    }

    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="T">Foldable type</typeparam>
    /// <typeparam name="A">Bound values</typeparam>
    extension<T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where A : IAdditionOperators<A, A, A>, IAdditiveIdentity<A, A>
    {
        /// <summary>
        /// Find sum of all the values in the structure
        /// </summary>
        public A Sum() =>
            T.Fold((s, x) => s + x, A.AdditiveIdentity, ta);
    }

    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="T">Foldable type</typeparam>
    /// <typeparam name="A">Bound values</typeparam>
    extension<T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where A : IMultiplyOperators<A, A, A>, IMultiplicativeIdentity<A, A>
    {
        /// <summary>
        /// Find product of all the values in the structure
        /// </summary>
        public A Product() =>
            T.Fold((s, x) => s * x, A.MultiplicativeIdentity, ta);
    }
    
    /// <param name="ta">Foldable structure</param>
    /// <typeparam name="T">Foldable type</typeparam>
    /// <typeparam name="A">Bound values</typeparam>
    extension<T, A>(LT.K<T, A> ta)
        where T : Foldable<T>
        where A : INumber<A>
    {
        /// <summary>
        /// Find the average of all the values in the structure
        /// </summary>
        public A Average() =>
            T.Fold((s, x) => (s.Count + A.One, s.Total + x), (Count: A.Zero, Total: A.Zero), ta) switch
            {
                var (count, _) when count == A.Zero => A.Zero,
                var (count, total)                  => total / count
            };
    }
}
