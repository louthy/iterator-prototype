using LanguageExt;
using LanguageExt.Traits;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

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
    static abstract Iterator<A> Forward<A>(K<T, A> ta);

    /// <summary>
    /// Bounce the iterable to a span
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    [Pure]
    static virtual ReadOnlySpan<A> AsSpan<A>(K<T, A> ta)
    {
        var w  = ArrayWriter<A>.Init();
        T.ToWriter(ta, ref w);
        return w.View;
    }
        
    /// <summary>
    /// Write every element of this iterable to the `ArrayWriter` provided
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="writer">Writer to emit the elements to</param>
    static virtual Unit ToWriter<A>(K<T, A> ta, ref ArrayWriter<A> writer)
    {
        var ts = T.Forward(ta);
        while (ts.TryGetValue(out var x, out ts))
        {
            ArrayWriter<A>.Add(ref writer, x);
        }
        return default;
    }
        
    /// <summary>
    /// Write every element of this iterable to the `ArrayWriter` provided
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="f">Map function</param>
    /// <param name="writer">Writer to emit the elements to</param>
    static virtual Unit ToWriter<A, B>(K<T, A> ta, Func<A, B> f, ref ArrayWriter<B> writer)
    {
        var ts = T.Forward(ta);
        while (ts.TryGetValue(out var x, out ts))
        {
            ArrayWriter<B>.Add(ref writer, f(x));
        }
        return default;
    }

    /// <summary>
    /// Provide a lazy enumerable
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    [Pure]
    static virtual IEnumerable<A> AsEnumerable<A>(K<T, A> ta)
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
    static virtual IterableEnumerator<T, A> GetEnumerator<A>(K<T, A> ta) =>
        new (ta);

    /// <summary>
    /// Returns `true` if the iterable is empty, `false` otherwise. `Nil` is a unit-value that makes pattern-matching
    /// more declarative.
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="nil">Nil</param>
    /// <returns>`true` if the iterable is empty, `false` otherwise</returns>
    [Pure]
    static virtual bool TryGetValue<A>(K<T, A> ta, out Nil nil)
    {
        var i = T.Forward(ta);
        return i.TryGetValue(out nil);
    }

    /// <summary>
    /// Returns the first element of the iterable and the rest of the iterable in a `Cons` structure.
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="cons">Cons structure containing head and tail values</param>
    /// <returns>`true` if it was possible to extract a head value.</returns>
    [Pure]
    static virtual bool TryGetValue<A>(K<T, A> ta, out Cons<A> cons)
    {
        var i = T.Forward(ta);
        return i.TryGetValue(out cons);
    }
    
    /// <summary>
    /// Returns the first element of the iterable and the rest of the iterable in a `Cons` structure.
    /// </summary>
    /// <param name="ta">Iterable structure</param>
    /// <param name="head">Head value</param>
    /// <param name="tail">Tail iterator</param>
    /// <returns>`true` if it was possible to extract a head value.</returns>
    [Pure]
    static virtual bool TryGetValue<A>(K<T, A> ta, out A head, out Iterator<A> tail)
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
    static virtual string ToString<A>(K<T, A> ta, string separator = ", ")
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
    static virtual string ToArrayString<A>(K<T, A> ta, string separator = ", ")
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
    static virtual string ToFullString<A>(K<T, A> ta, string separator = ", ")
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
    static virtual string ToFullArrayString<A>(K<T, A> ta, string separator = ", ")
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual K<T, A> Do<A>(K<T, A> ta, Action<A> f)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual ReadOnlySpan<A> Sort<A>(K<T, A> ta)
    {
        var w = ArrayWriter<A>.Init();
        T.ToWriter(ta, ref w);
        var s = w.MutableView;
        s.Sort();
        return s;
    }

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>ReadOnlySpan</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual ReadOnlySpan<A> Sort<A>(K<T, A> ta, Comparison<A> comparer)
    {
        var w = ArrayWriter<A>.Init();
        T.ToWriter(ta, ref w);
        var s = w.MutableView;
        s.Sort(comparer);
        return s;
    }

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>ReadOnlySpan</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual ReadOnlySpan<A> SortBy<A, Key>(K<T, A> ta, Func<A, Key> key, Comparison<Key> comparer)
    {
        // Blit the keys
        var kw = ArrayWriter<Key>.Init();
        T.ToWriter(ta, key, ref kw);
        var k = kw.MutableView;

        // Blit the values 
        var sw = ArrayWriter<A>.Init(k.Length);
        T.ToWriter(ta, ref sw);
        var s = sw.MutableView;
        
        // Sort based on the keys
        k.Sort(s, comparer);
        
        return s;
    }
    
    
    
    
    
    
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    // LINQ operators
    // 

    /*
    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>Iterable</returns>
    static virtual Iterator<A> Order<A>(Comparison<A> comparer, K<T, A> fa) =>
        T.Forward(fa)
         .Order(comparer);

    /// <summary>
    /// Yield items in ascending order 
    /// </summary>
    /// <returns>Iterable</returns>
    static virtual Iterator<A> OrderBy<Key, A>(Func<A, Key> keySelector, Comparison<Key> comparer, K<T, A> fa) =>
        T.Forward(fa)
         .OrderBy(keySelector, comparer);*/

    /// <summary>
    /// Projects each element of a range into a new form.
    /// </summary>
    static virtual Iterator<B> Select<A, B>(Func<A, B> f, K<T, A> fa) =>
        T.Forward(fa).Select(f);

    /*
    /// <summary>
    /// Filters a range of values based on a predicate.
    /// </summary>
    static virtual Iterator<A> Where<A>(Func<A, bool> f, K<T, A> fa) =>
        T.Forward(fa)
         .Filter(f);

    /// <summary>
    /// Monadic bind
    /// </summary>
    static virtual Iterator<B> SelectMany<A, B>(Func<A, Iterator<B>> f, K<T, A> fa) =>
        T.Forward(fa)
         .Bind(f);

    /// <summary>
    /// Monadic bind and project
    /// </summary>
    static virtual Iterator<C> SelectMany<A, B, C>(Func<A, Iterator<B>> bind, Func<A, B, C> project, K<T, A> fa) =>
        T.Forward(fa)
         .SelectMany(x => bind(x).Select(y => project(x, y)));

    /// <summary>
    /// Applies an accumulator function over a range.
    /// </summary>
    static virtual S Aggregate<S, A>(S state, Func<S, A, S> folder, K<T, A> fa) =>
        T.Forward(fa)
         .Fold(folder, state);

    /// <summary>
    /// Determines whether any element of a range satisfies a condition.
    /// </summary>
    static virtual bool Any<A>(Func<A, bool> predicate, K<T, A> fa) =>
        T.Forward(fa)
         .Exists(predicate);

    /// <summary>
    /// Determines whether all elements of a range satisfy a condition.
    /// </summary>
    static virtual bool All<A>(Func<A, bool> predicate, K<T, A> fa) =>
        T.Forward(fa)
         .ForAll(predicate);

    /// <summary>
    /// Returns the first element of a range, or a default value if the range contains no elements.
    /// </summary>
    static virtual Option<A> FirstOrNone<A>(K<T, A> fa) =>
        T.Forward(fa) switch
        {
            (Exist<A> exist, _) => exist.Value,
            _                   => default
        };

    /// <summary>
    /// Returns the last element of a range, or a default value if the range contains no elements.
    /// </summary>
    static virtual Option<A> LastOrNone<A>(K<T, A> fa)
    {
        var iter = T.Forward(fa); 
        while (true)
        {
            switch (iter)
            {
                case (Exist<A>, (Exist<A>, _) e2):
                    iter = e2;
                    break;

                case (Exist<A> (var e1), _):
                    return e1;

                default:
                    return default;
            }
        }
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a range.
    /// </summary>
    static virtual Iterator<A> Skip<A>(long count, K<T, A> fa) =>
        T.Forward(fa).Skip(count);

    /// <summary>
    /// Skip items at the start of the sequence whilst the predicate returns true. 
    /// </summary>
    static virtual Iterator<A> SkipWhile<A>(Func<A, bool> predicate, K<T, A> fa) =>
        T.Forward(fa).SkipWhile(predicate);

    /// <summary>
    /// Skip items at the start of the sequence until the predicate returns true. 
    /// </summary>
    static virtual Iterator<A> SkipUntil<A>(Func<A, bool> predicate, K<T, A> fa) =>
        T.Forward(fa).SkipUntil(predicate);

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a range.
    /// </summary>
    static virtual Iterator<A> Take<A>(long count, K<T, A> fa) =>
        T.Forward(fa).Take(count);

    /// <summary>
    /// Take items from the sequence whilst the predicate returns true.   
    /// </summary>
    static virtual Iterator<A> TakeWhile<A>(Func<A, bool> predicate, K<T, A> fa) =>
        T.Forward(fa).TakeWhile(predicate);

    /// <summary>
    /// Take items from the sequence until the predicate returns true.   
    /// </summary>
    static virtual Iterator<A> TakeUntil<A>(Func<A, bool> predicate, K<T, A> fa) =>
        T.Forward(fa).TakeUntil(predicate);    

    /// <summary>
    /// Filter out items that don't match the specified type.    
    /// </summary>
    static virtual Iterator<B> Cast<A, B>(K<T, A> fa) =>
        T.Forward(fa).Cast<B>();

    /// <summary>
    /// Reverse the order of the elements in a range.    
    /// </summary>
    static virtual Iterator<A> Reverse<A>(K<T, A> fa) =>
        T.Forward(fa).Reverse();
    
    /// <summary>
    /// Return a range with duplicate elements removed.   
    /// </summary>
    static virtual Iterator<A> Distinct<EqA, A>(K<T, A> fa) 
        where EqA : Eq<A> =>
        T.Forward(fa).Distinct<EqA>();
    
    /// <summary>
    /// Return a range with duplicate elements removed.   
    /// </summary>
    static virtual Iterator<A> Distinct<EqA, A>(ReadOnlySpan<A> seen, K<T, A> fa) 
        where EqA : Eq<A> =>
        T.Forward(fa).Distinct<EqA>(seen);
    
    /// <summary>
    /// Return a range with duplicate elements removed.   
    /// </summary>
    static virtual Iterator<A> DistinctBy<EqKey, Key, A>(Func<A, Key> key, K<T, A> fa)  
        where EqKey : Eq<Key> =>
        T.Forward(fa).DistinctBy<EqKey, Key>(key);    
    
    /// <summary>
    /// Return a range with duplicate elements removed.   
    /// </summary>
    static virtual Iterator<A> DistinctBy<EqKey, Key, A>(Func<A, Key> key, ReadOnlySpan<Key> seen, K<T, A> fa)  
        where EqKey : Eq<Key> =>
        T.Forward(fa).DistinctBy<EqKey, Key>(key, seen);*/
}
