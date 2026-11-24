using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3;
using LanguageExt.ClassInstances;

namespace IteratorPrototype.Traits;

/// <summary>
/// Iterable structure
/// </summary>
/// <typeparam name="T">Trait self-type</typeparam>
public interface Iterable<T>
    where T : Iterable<T>
{
    /// <summary>
    /// Get the forward iterator
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <returns>An iterator that enumerates from the 'first' element to the 'last' element.</returns>
    [Pure]
    static abstract Iter<A> Forward<A>(LT.K<T, A> ta);

    /// <summary>
    /// Bounce the iterable to a span
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    [Pure]
    static virtual ReadOnlySpan<A> AsSpan<A>(LT.K<T, A> ta)
    {
        var w  = LE.ArrayWriter<A>.Init();
        T.ToWriter(ta, ref w);
        return w.View;
    }
        
    /// <summary>
    /// Write every element of this iterable to the `ArrayWriter` provided
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="writer">Writer to emit the elements to</param>
    static virtual LE.Unit ToWriter<A>(LT.K<T, A> ta, ref LE.ArrayWriter<A> writer)
    {
        var ts = T.Forward(ta);
        while (ts.TryGetValue(out var x, out ts))
        {
            LE.ArrayWriter<A>.Add(ref writer, x);
        }
        return default;
    }
        
    /// <summary>
    /// Write every element of this iterable to the `ArrayWriter` provided
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="f">Map function</param>
    /// <param name="writer">Writer to emit the elements to</param>
    static virtual LE.Unit ToWriter<A, B>(LT.K<T, A> ta, Func<A, B> f, ref LE.ArrayWriter<B> writer)
    {
        var ts = T.Forward(ta);
        while (ts.TryGetValue(out var x, out ts))
        {
            LE.ArrayWriter<B>.Add(ref writer, f(x));
        }
        return default;
    }

    /// <summary>
    /// Provide a lazy enumerable
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    [Pure]
    static virtual IEnumerable<A> AsEnumerable<A>(LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (ts.TryGetValue(out var x, out ts))
        {
            yield return x;
        }
    }
    
    /// <summary>
    /// Return an enumerator for this iterable
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    [Pure]
    static virtual IterableEnumerator<T, A> GetEnumerator<A>(LT.K<T, A> ta) =>
        new (ta);
    
    /// <summary>
    /// Returns the first element of the iterable and the rest of the iterable in a `Cons` structure.
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail iterator</param>
    /// <returns>`true` if it was possible to extract a head value.</returns>
    [Pure]
    static virtual bool TryGetValue<A>(LT.K<T, A> ta, out A head, out Iter<A> tail)
    {
        var i = T.Forward(ta);
        return i.TryGetValue(out head, out tail);
    }
    
    /// <summary>
    /// Convert to string 
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="separator">Text to separate each element by</param>
    /// <returns></returns>
    [Pure]
    static virtual string ToString<A>(LT.K<T, A> ta, string separator = ", ")
    {
        var ix   = 0;
        var sm   = new StringMaker(stackalloc char[1024]);
        var iter = T.Forward(ta);

        while (iter.TryGetValue(out var x, out iter))
        {
            sm.Append(x);
            ix++;

            if (ix == 50)
            {
                sm.Append("...  ");
                break;
            }
            else
            {
                sm.Append(separator);
            }
        }

        if (sm.Length > 0)
        {
            sm.Undo(2); // Remove the last separator
        }

        return sm.ToString();
    }
    
    /// <summary>
    /// Convert to string 
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="separator">Text to separate each element by</param>
    [Pure]
    static virtual string ToArrayString<A>(LT.K<T, A> ta, string separator = ", ")
    {
        var ix   = 0;
        var sm   = new StringMaker(stackalloc char[1024]);
        var iter = T.Forward(ta);

        sm.Append('[');
        while (iter.TryGetValue(out var x, out iter))
        {
            sm.Append(x);
            ix++;

            if (ix == 50)
            {
                sm.Append("...  ");
                break;
            }
            else
            {
                sm.Append(separator);
            }
        }

        if (sm.Length > 1)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }

        sm.Append(']');

        return sm.ToString();
    }

    /// <summary>
    /// Convert to string 
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="separator">Text to separate each element by</param>
    [Pure]
    static virtual string ToFullString<A>(LT.K<T, A> ta, string separator = ", ")
    {
        var sm   = new StringMaker(stackalloc char[1024]);
        var iter = T.Forward(ta);

        while (iter.TryGetValue(out var x, out iter))
        {
            sm.Append(x);
            sm.Append(separator);
        }
        if (sm.Length > 0)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }

        return sm.ToString();
    }

    /// <summary>
    /// Convert to string 
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="separator">Text to separate each element by</param>
    [Pure]
    static virtual string ToFullArrayString<A>(LT.K<T, A> ta, string separator = ", ")
    {
        var sm   = new StringMaker(stackalloc char[1024]);
        var iter = T.Forward(ta);

        sm.Append('[');
        while (iter.TryGetValue(out var x, out iter))
        {
            sm.Append(x);
            sm.Append(separator);
        }
        if (sm.Length > 1)
        {
            sm.Undo(separator.Length); // Remove the last separator
        }
        sm.Append(']');

        return sm.ToString();
    }
    
    /// <summary>
    /// Perform an action on each element of the iterable
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="f">Action to perform</param>
    /// <returns>The original unchanged structure</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    static virtual LT.K<T, A> Do<A>(LT.K<T, A> ta, Action<A> f)
    {
        var iter = ta.Forward();
        while(iter.TryGetValue(out var x, out iter))
        {
            f(x);
        }
        return ta;
    }

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>ReadOnlySpan</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    static virtual Arr<A> Sort<A>(LT.K<T, A> ta)
    {
        var w = LE.ArrayWriter<A>.Init();
        T.ToWriter(ta, ref w);
        var s = w.MutableView;
        s.Sort(OrdDefault<A>.Compare);
        return w.ToArr();
    }

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>ReadOnlySpan</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    static virtual Arr<A> Sort<A>(LT.K<T, A> ta, Comparison<A> comparer)
    {
        var w = LE.ArrayWriter<A>.Init();
        T.ToWriter(ta, ref w);
        var s = w.MutableView;
        s.Sort(comparer);
        return w.ToArr();
    }

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>ReadOnlySpan</returns>
    [Pure]
    [MethodImpl(Optimisations.InliningOnly)]
    static virtual Arr<A> SortBy<A, Key>(LT.K<T, A> ta, Func<A, Key> key, Comparison<Key> comparer)
    {
        // Blit the keys
        var kw = LE.ArrayWriter<Key>.Init();
        T.ToWriter(ta, key, ref kw);
        var k = kw.MutableView;

        // Blit the values 
        var sw = LE.ArrayWriter<A>.Init(k.Length);
        T.ToWriter(ta, ref sw);
        var s = sw.MutableView;
        
        // Sort based on the keys
        k.Sort(s, comparer);
        
        return sw.ToArr();
    }
    
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    // LINQ operators
    // 

    /// <summary>
    /// Projects each element of a range into a new form.
    /// </summary>
    static virtual Iter<B> Select<A, B>(Func<A, B> f, LT.K<T, A> fa) =>
        T.Forward(fa)
         .Map(f);

    /// <summary>
    /// Filters a range of values based on a predicate.
    /// </summary>
    static virtual Iter<A> Where<A>(Func<A, bool> f, LT.K<T, A> fa) =>
        T.Forward(fa)
         .Filter(f);

    /// <summary>
    /// Bind
    /// </summary>
    static virtual Iter<B> SelectMany<A, B>(Func<A, Iter<B>> f, LT.K<T, A> fa) =>
        T.Forward(fa)
         .Bind(f);

    /// <summary>
    /// Bind and project
    /// </summary>
    static virtual Iter<C> SelectMany<A, B, C>(Func<A, Iter<B>> bind, Func<A, B, C> project, LT.K<T, A> fa) =>
        T.Forward(fa)
         .SelectMany(bind, project);

    /// <summary>
    /// Applies an accumulator function over a range.
    /// </summary>
    static virtual S Aggregate<S, A>(S state, Func<S, A, S> folder, LT.K<T, A> fa)
    {
        foreach (var x in T.Forward(fa))
        {
            state = folder(state, x);
        }
        return state;
    }
    
    /// <summary>
    /// Applies an accumulator function over a range and then maps the result
    /// </summary>
    static virtual R Aggregate<S, A, R>(S state, Func<S, A, S> folder, Func<S, R> map, LT.K<T, A> fa)
    {
        foreach (var x in T.Forward(fa))
        {
            state = folder(state, x);
        }
        return map(state);
    }
    
    /// <summary>
    /// Determines whether any element of a range satisfies a condition.
    /// </summary>
    static virtual bool Any<A>(Func<A, bool> predicate, LT.K<T, A> fa) =>
        T.Forward(fa)
         .Any(predicate);

    /// <summary>
    /// Determines whether all elements of a range satisfy a condition.
    /// </summary>
    static virtual bool All<A>(Func<A, bool> predicate, LT.K<T, A> fa) =>
        T.Forward(fa)
         .All(predicate);

    /// <summary>
    /// Returns the first element of a range, or an alternative value if the range contains no elements.
    /// </summary>
    static virtual LT.K<F, A> FirstOrAlt<F, A>(LT.K<T, A> fa) 
        where F : LT.Alternative<F> =>
        T.Forward(fa)
         .FirstOrAlt<F>();

    /// <summary>
    /// Returns the first element of a range, or `None` value if the range contains no elements.
    /// </summary>
    static virtual LE.Option<A> FirstOrNone<A>(LT.K<T, A> fa) =>
        T.Forward(fa)
         .FirstOrNone();

    /// <summary>
    /// Returns the first element of a range, or a default value if the range contains no elements.
    /// </summary>
    static virtual A? FirstOrDefault<A>(LT.K<T, A> fa) =>
        T.Forward(fa)
         .FirstOrDefault();

    /// <summary>
    /// Returns the last element of a range, or an alternative value if the range contains no elements.
    /// </summary>
    static virtual LT.K<F, A> LastOrAlt<F, A>(LT.K<T, A> fa) 
        where F : LT.Alternative<F> =>
        T.Forward(fa)
         .LastOrAlt<F>();

    /// <summary>
    /// Returns the last element of a range, or `None` value if the range contains no elements.
    /// </summary>
    static virtual LE.Option<A> LastOrNone<A>(LT.K<T, A> fa) =>
        T.Forward(fa)
         .LastOrNone();

    /// <summary>
    /// Returns the last element of a range, or a default value if the range contains no elements.
    /// </summary>
    static virtual A? LastOrDefault<A>(LT.K<T, A> fa) =>
        T.Forward(fa)
         .LastOrDefault();

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a range.
    /// </summary>
    static virtual Iter<A> Skip<A>(int count, LT.K<T, A> fa) =>
        T.Forward(fa)
         .Skip(count);

    /// <summary>
    /// Skip items at the start of the sequence whilst the predicate returns true.
    /// </summary>
    static virtual Iter<A> SkipWhile<A>(Func<A, bool> predicate, LT.K<T, A> fa) =>
        T.Forward(fa)
         .SkipWhile(predicate);

    /// <summary>
    /// Skip items at the start of the sequence until the predicate returns true.
    /// </summary>
    static virtual Iter<A> SkipUntil<A>(Func<A, bool> predicate, LT.K<T, A> fa) =>
        T.Forward(fa)
         .SkipUntil(predicate);

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a range.
    /// </summary>
    static virtual Iter<A> Take<A>(int count, LT.K<T, A> fa) =>
        T.Forward(fa)
         .Take(count);

    /// <summary>
    /// Take items from the sequence whilst the predicate returns true.
    /// </summary>
    static virtual Iter<A> TakeWhile<A>(Func<A, bool> predicate, LT.K<T, A> fa) =>
        T.Forward(fa)
         .TakeWhile(predicate);

    /// <summary>
    /// Take items from the sequence until the predicate returns true.
    /// </summary>
    static virtual Iter<A> TakeUntil<A>(Func<A, bool> predicate, LT.K<T, A> fa) =>
        T.Forward(fa)
         .TakeUntil(predicate);

    /// <summary>
    /// Filter out items that don't match the specified type.
    /// </summary>
    static virtual Iter<B> Cast<A, B>(LT.K<T, A> fa) =>
        T.Forward(fa)
         .Cast<B>();

    /// <summary>
    /// Reverse the order of the elements in a range.
    /// </summary>
    /// <remarks>The return-type is an array to indicate that: to reverse a set of values, all values must be available.
    /// Using `Reverse` with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN reversed.
    /// And because the resulting array-type is also an `Iterable`, all of the `Iterable` operators can continue to work
    /// on the reversed-set.
    /// </remarks>
    static virtual Arr<A> Reverse<A>(LT.K<T, A> fa)
    {
        var w = LE.ArrayWriter<A>.Init();
        foreach (var x in fa)
        {
            w.Add(x);
        }

        var (array, start, length) = w.ToArrayBack();
        return new Arr<A>(array, start, length);
    }

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <returns>Projected lazy iterator</returns>
    static virtual Iter<A> Distinct<EqA, A>(ReadOnlySpan<A> seen, LT.K<T, A> fa)
        where EqA : LT.Eq<A> =>
        T.Forward(fa)
         .Distinct<EqA>(seen);

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="key">Function that maps from the element-type to a key for distinct testing.</param>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <returns>Projected lazy iterator</returns>
    static virtual Iter<A> DistinctBy<EqKey, Key, A>(Func<A, Key> key, ReadOnlySpan<Key> seen, LT.K<T, A> fa)
        where EqKey : LT.Eq<Key> =>
        T.Forward(fa)
         .DistinctBy<EqKey, Key>(key, seen);
}
