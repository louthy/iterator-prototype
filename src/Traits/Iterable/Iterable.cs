using System.Diagnostics.Contracts;
using System.Numerics;
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
        var w = LE.ArrayWriter<A>.Init();
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
        while (ts.TryRef(out var x))
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
        while (ts.TryRef(out var x))
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
        while (ts.TryRef(out var x))
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
        new(ta);

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
        var ix = 0;
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.Forward(ta);

        while (ts.TryRef(out var x))
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
        var ix = 0;
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.Forward(ta);

        sm.Append('[');
        while (ts.TryRef(out var x))
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
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.Forward(ta);

        while (ts.TryRef(out var x))
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
        var sm = new StringMaker(stackalloc char[1024]);
        var ts = T.Forward(ta);

        sm.Append('[');
        while (ts.TryRef(out var x))
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
        var ts = ta.Forward();
        while (ts.TryRef(out var x))
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
    /// <param name="f">Mapping function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<B> Select<A, B>(Func<A, B> f, LT.K<T, A> ta) =>
        T.Forward(ta)
         .Map(f);

    /// <summary>
    /// Filters a range of values based on a predicate.
    /// </summary>
    /// <param name="f">Filtering function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> Where<A>(Func<A, bool> f, LT.K<T, A> ta) =>
        T.Forward(ta)
         .Filter(f);

    /// <summary>
    /// Bind
    /// </summary>
    /// <param name="f">Bind function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<B> SelectMany<A, B>(Func<A, Iter<B>> f, LT.K<T, A> ta) =>
        T.Forward(ta)
         .Bind(f);

    /// <summary>
    /// Bind and project
    /// </summary>
    /// <param name="bind">Bind function</param>
    /// <param name="project">Projection function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<C> SelectMany<A, B, C>(Func<A, Iter<B>> bind, Func<A, B, C> project, LT.K<T, A> ta) =>
        T.Forward(ta)
         .SelectMany(bind, project);

    /// <summary>
    /// Applies an accumulator function over a range.
    /// </summary>
    /// <param name="state">Initial state</param>
    /// <param name="folder">Aggregation function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual S Aggregate<S, A>(S state, Func<S, A, S> folder, LT.K<T, A> ta)
    {
        foreach (var x in T.Forward(ta))
        {
            state = folder(state, x);
        }

        return state;
    }

    /// <summary>
    /// Applies an accumulator function over a range and then maps the result
    /// </summary>
    /// <param name="state">Initial state</param>
    /// <param name="folder">Aggregation function</param>
    /// <param name="map">Result mapping function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual R Aggregate<S, A, R>(S state, Func<S, A, S> folder, Func<S, R> map, LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (ts.TryRef(out var x))
        {
            state = folder(state, x);
        }

        return map(state);
    }

    /// <summary>
    /// Determines whether any element of a range satisfies a condition.
    /// </summary>
    /// <param name="predicate">Predicate function that tests the condition</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual bool Any<A>(Func<A, bool> predicate, LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (ts.TryRef(out var x))
        {
            if (predicate(x)) return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether all elements of a range satisfy a condition.
    /// </summary>
    /// <param name="predicate">Predicate function that tests the condition</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual bool All<A>(Func<A, bool> predicate, LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (ts.TryRef(out var x))
        {
            if (!predicate(x)) return false;
        }

        return true;
    }

    /// <summary>
    /// Finds the average of a sequence of numbers
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual A Average<A>(LT.K<T, A> ta)
        where A : INumber<A>
    {
        var ts    = T.Forward(ta);
        var total = A.Zero;
        var count = A.Zero;
        while (ts.TryRef(out var x))
        {
            total += x;
            count++;
        }

        return total / count;
    }

    /// <summary>
    /// Finds the average of a sequence of numbers
    /// </summary>
    /// <param name="map">Mapping function</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual B Average<A, B>(Func<A, B> map, LT.K<T, A> ta)
        where B : INumber<B>
    {
        var ts    = T.Forward(ta);
        var total = B.Zero;
        var count = B.Zero;
        while (ts.TryRef(out var x))
        {
            total += map(x);
            count++;
        }

        return total / count;
    }

    /// <summary>
    /// Returns the first element of a range, or an alternative value if the range contains no elements.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual LT.K<F, A> FirstOrAlt<F, A>(LT.K<T, A> ta)
        where F : LT.Alternative<F> =>
        T.Forward(ta)
         .TryGetValue(out var x, out _)
            ? F.Pure(x)
            : F.Empty<A>();

    /// <summary>
    /// Returns the first element of a range, or `None` value if the range contains no elements.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual LE.Option<A> FirstOrNone<A>(LT.K<T, A> ta) =>
        T.Forward(ta)
         .TryGetValue(out var x, out _)
            ? x
            : default;

    /// <summary>
    /// Returns the first element of a range, or a default value if the range contains no elements.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual A? FirstOrDefault<A>(LT.K<T, A> ta) =>
        T.Forward(ta)
         .TryGetValue(out var x, out _)
            ? x
            : default;

    /// <summary>
    /// Returns the last element of a range, or an alternative value if the range contains no elements.
    /// <param name="ta">Iterable sequence</param>
    /// </summary>
    static virtual LT.K<F, A> LastOrAlt<F, A>(LT.K<T, A> ta)
        where F : LT.Alternative<F>
    {
        var ts = T.Forward(ta);
        while (true)
        {
            switch (ts)
            {
                case Cons<A>(_, Cons<A>(_, _) e2):
                    ts = e2;
                    break;

                case Cons<A> (var e1, _):
                    return F.Pure(e1);

                default:
                    return F.Empty<A>();
            }
        }
    }

    /// <summary>
    /// Returns the last element of a range, or `None` value if the range contains no elements.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual LE.Option<A> LastOrNone<A>(LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (true)
        {
            switch (ts)
            {
                case Cons<A>(_, Cons<A>(_, _) e2):
                    ts = e2;
                    break;

                case Cons<A> (var e1, _):
                    return e1;

                default:
                    return default;
            }
        }
    }

    /// <summary>
    /// Returns the last element of a range, or a default value if the range contains no elements.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual A? LastOrDefault<A>(LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (true)
        {
            switch (ts)
            {
                case Cons<A>(_, Cons<A>(_, _) e2):
                    ts = e2;
                    break;

                case Cons<A> (var e1, _):
                    return e1;

                default:
                    return default;
            }
        }
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a range.
    /// </summary>
    /// <param name="count">Number of elements to skip</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> Skip<A>(int count, LT.K<T, A> ta) =>
        T.Forward(ta)
         .Skip(count);

    /// <summary>
    /// Skip items at the start of the sequence whilst the predicate returns true.
    /// </summary>
    /// <param name="predicate">Keep skipping elements while this predicate returns `true`</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> SkipWhile<A>(Func<A, bool> predicate, LT.K<T, A> ta) =>
        T.Forward(ta)
         .SkipWhile(predicate);

    /// <summary>
    /// Skip items at the start of the sequence until the predicate returns true.
    /// </summary>
    /// <param name="predicate">Keep skipping elements until this predicate returns `true`</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> SkipUntil<A>(Func<A, bool> predicate, LT.K<T, A> ta) =>
        T.Forward(ta)
         .SkipUntil(predicate);

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a range.
    /// </summary>
    /// <param name="count">Number of elements to take</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> Take<A>(int count, LT.K<T, A> ta) =>
        T.Forward(ta)
         .Take(count);

    /// <summary>
    /// Take items from the sequence whilst the predicate returns true.
    /// </summary>
    /// <param name="predicate">Keep taking elements while this predicate returns `true`</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> TakeWhile<A>(Func<A, bool> predicate, LT.K<T, A> ta) =>
        T.Forward(ta)
         .TakeWhile(predicate);

    /// <summary>
    /// Take items from the sequence until the predicate returns true.
    /// </summary>
    /// <param name="predicate">Keep taking elements until this predicate returns `true`</param>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<A> TakeUntil<A>(Func<A, bool> predicate, LT.K<T, A> ta) =>
        T.Forward(ta)
         .TakeUntil(predicate);

    /// <summary>
    /// Filter out items that don't match the specified type.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    static virtual Iter<B> Cast<A, B>(LT.K<T, A> ta) =>
        T.Forward(ta)
         .Cast<B>();

    /// <summary>
    /// Reverse the order of the elements in a range.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    /// <remarks>The return-type is an array to indicate that: to reverse a set of values, all values must be available.
    /// Using `Reverse` with an infinite sequence, for example, will just fill up memory.
    /// The array return value highlights that all values will be acquired from the stream AND THEN reversed.
    /// And because the resulting array-type is also an `Iterable`, all of the `Iterable` operators can continue to work
    /// on the reversed-set.
    /// </remarks>
    static virtual Arr<A> Reverse<A>(LT.K<T, A> ta)
    {
        var w  = LE.ArrayWriter<A>.Init();
        var ts = T.Forward(ta);
        while (ts.TryRef(out var x))
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
    /// <param name="ta">Iterable sequence</param>
    /// <returns>Projected lazy iterator</returns>
    static virtual Iter<A> Distinct<EqA, A>(ReadOnlySpan<A> seen, LT.K<T, A> ta)
        where EqA : LT.Eq<A> =>
        T.Forward(ta)
         .Distinct<EqA>(seen);

    /// <summary>
    /// Return a range with duplicate elements removed.
    /// </summary>
    /// <param name="key">Function that maps from the element-type to a key for distinct testing.</param>
    /// <param name="seen">Allows pre-seeding of the distinct-set with elements that have already been seen.</param>
    /// <param name="ta">Iterable sequence</param>
    /// <returns>Projected lazy iterator</returns>
    static virtual Iter<A> DistinctBy<EqKey, Key, A>(Func<A, Key> key, ReadOnlySpan<Key> seen, LT.K<T, A> ta)
        where EqKey : LT.Eq<Key> =>
        T.Forward(ta)
         .DistinctBy<EqKey, Key>(key, seen);

    /// <summary>
    /// Append a value to the sequence
    /// </summary>
    /// <param name="value">Non-null value to append</param>
    /// <param name="ta">Iterable sequence</param>
    /// <returns>Transformed lazy iterator</returns>
    static virtual Iter<A> Append<A>(in A value, LT.K<T, A> ta) =>
        value is null
            ? T.Forward(ta)
            : T.Forward(ta) + value;

    /// <summary>
    /// Prepend a value to the sequence
    /// </summary>
    /// <param name="value">Non-null value to prepend</param>
    /// <param name="ta">Iterable sequence</param>
    /// <returns>Transformed lazy iterator</returns>
    static virtual Iter<A> Prepend<A>(in A value, LT.K<T, A> ta) =>
        value is null
            ? T.Forward(ta)
            : value + T.Forward(ta);

    /// <summary>
    /// Concatenate two iterable sequences
    /// </summary>
    /// <param name="tx">First sequence</param>
    /// <param name="ty">Second sequence</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>Concatenated sequence</returns>
    static virtual Iter<A> Concat<A>(LT.K<T, A> tx, LT.K<T, A> ty) =>
        Iter.flatten(tx.Forward(), ty.Forward());

    /// <summary>
    /// Returns true if the `value` is contained in the iterable sequence
    /// </summary>
    /// <remarks>
    /// Stops processing the sequence upon discovery of a matching value.
    /// </remarks>
    /// <param name="value">Value to find</param>
    /// <param name="ta">Iterable sequence</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>True if the `value` is contained in the iterable sequence</returns>
    static virtual bool Contains<A>(in A value, LT.K<T, A> ta) =>
        T.Contains(in value, EqDefault<A>.Equals, ta);

    /// <summary>
    /// Returns true if the `value` is contained in the iterable sequence
    /// </summary>
    /// <remarks>
    /// Stops processing the sequence upon discovery of a matching value.
    /// </remarks>
    /// <param name="value">Value to find</param>
    /// <param name="ta">Iterable sequence</param>
    /// <typeparam name="EqA">Equality trait</typeparam>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>True if the `value` is contained in the iterable sequence</returns>
    static virtual bool Contains<EqA, A>(in A value, LT.K<T, A> ta)
        where EqA : LT.Eq<A> =>
        T.Contains(in value, EqA.Equals, ta);

    /// <summary>
    /// Returns true if the `value` is contained in the iterable sequence
    /// </summary>
    /// <remarks>
    /// Stops processing the sequence upon discovery of a matching value.
    /// </remarks>
    /// <param name="value">Value to find</param>
    /// <param name="eq">Equality comparer, for bespoke equality testing</param>
    /// <param name="ta">Iterable sequence</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>True if the `value` is contained in the iterable sequence</returns>
    static virtual bool Contains<A>(in A value, IEqualityComparer<A> eq, LT.K<T, A> ta) =>
        T.Contains(in value, eq.Equals, ta);

    /// <summary>
    /// Returns true if the `value` is contained in the iterable sequence
    /// </summary>
    /// <remarks>
    /// Stops processing the sequence upon discovery of a matching value.
    /// </remarks>
    /// <param name="value">Value to find</param>
    /// <param name="eq">Equality comparer function, for bespoke equality testing</param>
    /// <param name="ta">Iterable sequence</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>True if the `value` is contained in the iterable sequence</returns>
    static virtual bool Contains<A>(in A value, Func<A, A, bool> eq, LT.K<T, A> ta)
    {
        var ts = T.Forward(ta);
        while (ts.TryRef(out var x))
        {
            if (eq(x, value)) return true;
        }

        return false;
    }

    /// <summary>
    /// Find the minimum value in the structure
    /// </summary>
    static virtual LE.Option<A> Min<OrdA, A>(LT.K<T, A> ta)
        where OrdA : LT.Ord<A>
    {
        A   current;
        var iter = T.Forward(ta);
        if (iter is Cons<A>(var h, var t))
        {
            current = h;
        }
        else
        {
            return default;
        }

        foreach (var head in t)
        {
            if (OrdA.Compare(head, current) < 0) current = head;
        }

        return current;
    }

    /// <summary>
    /// Find the maximum value in the structure
    /// </summary>
    static virtual LE.Option<A> Max<OrdA, A>(LT.K<T, A> ta)
        where OrdA : LT.Ord<A>
    {
        A   current;
        var iter = T.Forward(ta);
        if (iter is Cons<A>(var h, var t))
        {
            current = h;
        }
        else
        {
            return default;
        }

        foreach (var head in t)
        {
            if (OrdA.Compare(head, current) > 0) current = head;
        }

        return current;
    }

    /// <summary>
    /// Returns the elements of the specified sequence or the default value for the element-type in a singleton
    /// collection if the sequence is empty.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>The elements of the specified sequence or the default value for the element-type in a singleton
    /// collection if the sequence is empty.</returns>
    static virtual Iter<A?> DefaultIfEmpty<A>(LT.K<T, A> ta) =>
        T.Forward(ta)
         .TryGetValue(out var x, out var xs)
            ? Iter.cons(x, xs.ForceCast<A?>())
            : Iter.singleton<A?>(default);

    /// <summary>
    /// Returns the elements of the specified sequence or the specified value in a singleton
    /// collection if the sequence is empty.
    /// </summary>
    /// <param name="ta">Iterable sequence</param>
    /// <param name="defaultValue">The value to return if the sequence is empty.</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>The elements of the specified sequence or the specified value in a singleton
    /// collection if the sequence is empty.</returns>
    static virtual Iter<A> DefaultIfEmpty<A>(A defaultValue, LT.K<T, A> ta) =>
        T.Forward(ta)
         .TryGetValue(out var x, out var xs)
            ? Iter.cons(x, xs)
            : Iter.singleton(defaultValue);

    /// <summary>
    /// Produces the set difference of two sequences.
    /// </summary>
    /// <remarks>
    /// The set difference of two sets is defined as the members of the first set that don't appear in the second set.
    /// This method returns those elements in `ta` that don't appear in `tb`. It doesn't return those elements in `tb
    /// that don't appear in `ta`. Only unique elements are returned.
    /// </remarks>
    /// <param name="ta">First sequence</param>
    /// <param name="tb">Second sequence</param>
    /// <typeparam name="EqA">Equality trait</typeparam>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>An array that contains the set difference of the elements of two sequences.</returns>
    static virtual Arr<A> Except<EqA, A>(LT.K<T, A> ta, LT.K<T, A> tb)
        where EqA : LT.Eq<A> =>
        T.Except(ta, tb, LE.Eq.Comparer<EqA, A>());

    /// <summary>
    /// Produces the set difference of two sequences.
    /// </summary>
    /// <remarks>
    /// The set difference of two sets is defined as the members of the first set that don't appear in the second set.
    /// This method returns those elements in `ta` that don't appear in `tb`. It doesn't return those elements in `tb
    /// that don't appear in `ta`. Only unique elements are returned.
    /// </remarks>
    /// <param name="ta">First sequence</param>
    /// <param name="tb">Second sequence</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>An array that contains the set difference of the elements of two sequences.</returns>
    static virtual Arr<A> Except<A>(LT.K<T, A> ta, LT.K<T, A> tb) =>
        T.Except(ta, tb, LE.Eq.Comparer<EqDefault<A>, A>());

    /// <summary>
    /// Produces the set difference of two sequences.
    /// </summary>
    /// <remarks>
    /// The set difference of two sets is defined as the members of the first set that don't appear in the second set.
    /// This method returns those elements in `ta` that don't appear in `tb`. It doesn't return those elements in `tb
    /// that don't appear in `ta`. Only unique elements are returned.
    /// </remarks>
    /// <param name="ta">First sequence</param>
    /// <param name="tb">Second sequence</param>
    /// <param name="comparer">Equality comparer</param>
    /// <typeparam name="A">Element type</typeparam>
    /// <returns>An array that contains the set difference of the elements of two sequences.</returns>
    static virtual Arr<A> Except<A>(LT.K<T, A> ta, LT.K<T, A> tb, IEqualityComparer<A> comparer)
    {
        var xa    = new HashSet<A>(comparer);
        var count = 0;
        foreach (var a in ta)
        {
            if (!xa.Contains(a)) count++;
            xa.Add(a);
            count++;
        }

        foreach (var b in tb)
        {
            if (xa.Contains(b)) count--;
            xa.Remove(b);
        }

        var arr = new A[count];
        var w   = new LE.ArrayWriter<A>(arr, 0, false);
        foreach (var x in xa)
        {
            w.Add(x);
        }

        return w.ToArr();
    }

    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key. The elements of each group are projected by using a specified function.
    /// </summary>
    static virtual Iter<C> GroupBy<Key, A, B, C>(
        Func<A, Key> keySelector,
        Func<A, B> elementSelector,
        Func<Key, Iter<B>, C> resultSelector,
        IEqualityComparer<Key> comparer,
        LT.K<T, A> ta)
        where Key : notnull
    {
        var dict = new Dictionary<Key, List<B>>(comparer);
        var ts   = T.Forward(ta);
        while (ts.TryRef(out var x))
        {
            var k = keySelector(x);
            var b = elementSelector(x);
            if (dict.ContainsKey(k))
            {
                dict[k].Add(b);
            }
            else
            {
                dict.Add(k, [b]);
            }
        }

        return Iter.from([.. dict])
                   .Select(kv => resultSelector(kv.Key, Iter.from(dict[kv.Key])));
    }
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key. The elements of each group are projected by using a specified function.
    /// </summary>
    static virtual Iter<C> GroupBy<Key, A, B, C>(
        Func<A, Key> keySelector,
        Func<A, B> elementSelector,
        Func<Key, Iter<B>, C> resultSelector,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector, elementSelector, resultSelector, LE.Eq.Comparer<EqDefault<Key>, Key>(), ta);
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key. The elements of each group are projected by using a specified function.
    /// </summary>
    static virtual Iter<C> GroupBy<EqKey, Key, A, B, C>(
        Func<A, Key> keySelector,
        Func<A, B> elementSelector,
        Func<Key, Iter<B>, C> resultSelector,
        LT.K<T, A> ta)
        where Key : notnull
        where EqKey : LT.Eq<Key> =>
        T.GroupBy(keySelector, elementSelector, resultSelector, LE.Eq.Comparer<EqKey, Key>(), ta);

    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key. The elements of each group are projected by using a specified function.
    /// </summary>
    static virtual Iter<Group<Key, B>> GroupBy<Key, A, B>(
        Func<A, Key> keySelector,
        Func<A, B> elementSelector,
        IEqualityComparer<Key> comparer,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector: keySelector, 
                  elementSelector: elementSelector , 
                  resultSelector: (k, xs) => new Group<Key, B>(k, xs), 
                  comparer, 
                  ta); 
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key. The elements of each group are projected by using a specified function.
    /// </summary>
    static virtual Iter<Group<Key, B>> GroupBy<Key, A, B>(
        Func<A, Key> keySelector,
        Func<A, B> elementSelector,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector, elementSelector, LE.Eq.Comparer<EqDefault<Key>, Key>(), ta);
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key. The elements of each group are projected by using a specified function.
    /// </summary>
    static virtual Iter<Group<Key, B>> GroupBy<EqKey, Key, A, B>(
        Func<A, Key> keySelector,
        Func<A, B> elementSelector,
        LT.K<T, A> ta)
        where Key : notnull
        where EqKey : LT.Eq<Key> =>
        T.GroupBy(keySelector, elementSelector, LE.Eq.Comparer<EqKey, Key>(), ta);

    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key.
    /// </summary>
    static virtual Iter<B> GroupBy<Key, A, B>(
        Func<A, Key> keySelector,
        Func<Key, Iter<A>, B> resultSelector,
        IEqualityComparer<Key> comparer,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector: keySelector, 
                  elementSelector: x => x, 
                  resultSelector: resultSelector, 
                  comparer, 
                  ta); 
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key.
    /// </summary>
    static virtual Iter<B> GroupBy<Key, A, B>(
        Func<A, Key> keySelector,
        Func<Key, Iter<A>, B> resultSelector,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector, resultSelector, LE.Eq.Comparer<EqDefault<Key>, Key>(), ta);
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and creates a result value
    /// from each group and its key.
    /// </summary>
    static virtual Iter<B> GroupBy<EqKey, Key, A, B>(
        Func<A, Key> keySelector,
        Func<Key, Iter<A>, B> resultSelector,
        LT.K<T, A> ta)
        where Key : notnull
        where EqKey : LT.Eq<Key> =>
        T.GroupBy(keySelector, resultSelector, LE.Eq.Comparer<EqKey, Key>(), ta);    

    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function.
    /// </summary>
    static virtual Iter<Group<Key, A>> GroupBy<Key, A>(
        Func<A, Key> keySelector,
        IEqualityComparer<Key> comparer,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector: keySelector, 
                  elementSelector: x => x , 
                  resultSelector: (k, xs) => new Group<Key, A>(k, xs), 
                  comparer, 
                  ta);     
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function.
    /// </summary>
    static virtual Iter<Group<Key, A>> GroupBy<Key, A>(
        Func<A, Key> keySelector,
        LT.K<T, A> ta)
        where Key : notnull =>
        T.GroupBy(keySelector, LE.Eq.Comparer<EqDefault<Key>, Key>(), ta);
    
    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function.
    /// </summary>
    static virtual Iter<Group<Key, A>> GroupBy<EqKey, Key, A>(
        Func<A, Key> keySelector,
        LT.K<T, A> ta)
        where Key : notnull
        where EqKey : LT.Eq<Key> =>
        T.GroupBy(keySelector, LE.Eq.Comparer<EqKey, Key>(), ta);

    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// </summary>
    /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
    /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
    /// <param name="resultSelector">A function to create a result element from an element from the first sequence and
    /// a collection of matching elements from the second sequence.</param>
    /// <param name="comparer">Bespoke comparer for comparing the keys</param>
    /// <param name="ta">The first sequence to join.</param>
    /// <param name="tb">The sequence to join to the first sequence.</param>
    /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
    /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
    /// <typeparam name="C">The type of the result elements.</typeparam>
    /// <returns>An iterator that contains elements of type `C` that are obtained by performing a grouped join
    /// on two sequences.</returns>
    static virtual Iter<C> GroupJoin<Key, A, B, C>(
        Func<A, Key> keySelectorA,
        Func<B, Key> keySelectorB,
        Func<A, Iter<B>, C> resultSelector,
        IEqualityComparer<Key> comparer,
        LT.K<T, A> ta,
        LT.K<T, B> tb)
        where Key : notnull
    {
        var lookup = new Dictionary<Key, List<B>>(comparer);
        var tsb    = T.Forward(tb);
        while (tsb.TryRef(out var y))
        {
            var ky = keySelectorB(y);
            
            if (lookup.ContainsKey(ky))
            {
                lookup[ky].Add(y);
            }
            else
            {
                lookup.Add(ky, [y]);
            }
        }

        return T.Forward(ta)
                .Map(x => lookup.TryGetValue(keySelectorA(x), out var ys)
                              ? resultSelector(x, Iter.from(ys))
                              : resultSelector(x, Iter.empty));
    }

    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// </summary>
    /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
    /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
    /// <param name="resultSelector">A function to create a result element from an element from the first sequence and
    /// a collection of matching elements from the second sequence.</param>
    /// <param name="ta">The first sequence to join.</param>
    /// <param name="tb">The sequence to join to the first sequence.</param>
    /// <typeparam name="EqKey">Key equality trait.</typeparam>
    /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
    /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
    /// <typeparam name="C">The type of the result elements.</typeparam>
    /// <returns>An iterator that contains elements of type `C` that are obtained by performing a grouped join
    /// on two sequences.</returns>
    static virtual Iter<C> GroupJoin<EqKey, Key, A, B, C>(
        Func<A, Key> keySelectorA,
        Func<B, Key> keySelectorB,
        Func<A, Iter<B>, C> resultSelector,
        LT.K<T, A> ta,
        LT.K<T, B> tb)
        where EqKey : LT.Eq<Key>
        where Key : notnull =>
        T.GroupJoin(keySelectorA, keySelectorB, resultSelector, LE.Eq.Comparer<EqKey, Key>(), ta, tb);

    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// </summary>
    /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
    /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
    /// <param name="comparer">Bespoke comparer for comparing the keys</param>
    /// <param name="ta">The first sequence to join.</param>
    /// <param name="tb">The sequence to join to the first sequence.</param>
    /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
    /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
    /// <returns>An iterator that contains grouping elements that are obtained by performing a grouped join
    /// on two sequences.</returns>
    static virtual Iter<Group<A, B>> GroupJoin<Key, A, B>(
        Func<A, Key> keySelectorA,
        Func<B, Key> keySelectorB,
        IEqualityComparer<Key> comparer,
        LT.K<T, A> ta,
        LT.K<T, B> tb)
        where Key : notnull =>
        T.GroupJoin(keySelectorA, 
                    keySelectorB, 
                    (x, ys) => new Group<A, B>(x, ys), 
                    comparer, 
                    ta, 
                    tb);
    
    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// </summary>
    /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
    /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
    /// <param name="ta">The first sequence to join.</param>
    /// <param name="tb">The sequence to join to the first sequence.</param>
    /// <typeparam name="EqKey">Key equality trait.</typeparam>
    /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
    /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
    /// <returns>An iterator that contains grouping elements that are obtained by performing a grouped join
    /// on two sequences.</returns>
    static virtual Iter<Group<A, B>> GroupJoin<EqKey, Key, A, B>(
        Func<A, Key> keySelectorA,
        Func<B, Key> keySelectorB,
        LT.K<T, A> ta,
        LT.K<T, B> tb)
        where EqKey : LT.Eq<Key>
        where Key : notnull =>
        T.GroupJoin(keySelectorA, 
                    keySelectorB, 
                    (x, ys) => new Group<A, B>(x, ys), 
                    LE.Eq.Comparer<EqKey, Key>(), 
                    ta, 
                    tb);

    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// </summary>
    /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
    /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
    /// <param name="resultSelector">A function to create a result element from an element from the first sequence and
    /// a collection of matching elements from the second sequence.</param>
    /// <param name="ta">The first sequence to join.</param>
    /// <param name="tb">The sequence to join to the first sequence.</param>
    /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
    /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
    /// <typeparam name="C">The type of the result elements.</typeparam>
    /// <returns>An iterator that contains elements of type `C` that are obtained by performing a grouped join
    /// on two sequences.</returns>
    static virtual Iter<C> GroupJoin<Key, A, B, C>(
        Func<A, Key> keySelectorA,
        Func<B, Key> keySelectorB,
        Func<A, Iter<B>, C> resultSelector,
        LT.K<T, A> ta,
        LT.K<T, B> tb)
        where Key : notnull =>
        T.GroupJoin(keySelectorA, keySelectorB, resultSelector, LE.Eq.Comparer<EqDefault<Key>, Key>(), ta, tb);    
    
    /// <summary>
    /// Correlates the elements of two sequences based on key equality and groups the results.
    /// </summary>
    /// <param name="keySelectorA">A function to extract the join key from each element of the first sequence.</param>
    /// <param name="keySelectorB">A function to extract the join key from each element of the second sequence.</param>
    /// <param name="ta">The first sequence to join.</param>
    /// <param name="tb">The sequence to join to the first sequence.</param>
    /// <typeparam name="Key">The type of the keys returned by the key selector functions.</typeparam>
    /// <typeparam name="A">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="B">The type of the elements of the second sequence.</typeparam>
    /// <returns>An iterator that contains grouping elements that are obtained by performing a grouped join
    /// on two sequences.</returns>
    static virtual Iter<Group<A, B>> GroupJoin<Key, A, B>(
        Func<A, Key> keySelectorA,
        Func<B, Key> keySelectorB,
        LT.K<T, A> ta,
        LT.K<T, B> tb)
        where Key : notnull =>
        T.GroupJoin(keySelectorA, 
                    keySelectorB, 
                    (x, ys) => new Group<A, B>(x, ys), 
                    LE.Eq.Comparer<EqDefault<Key>, Key>(), 
                    ta, 
                    tb);
    
}
