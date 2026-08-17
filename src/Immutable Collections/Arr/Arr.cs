using System.Numerics;
using System.Diagnostics;
using LanguageExt.Traits;
using IteratorPrototype.Traits;
using LanguageExt.ClassInstances;
using static LanguageExt.Prelude;
using System.Diagnostics.Contracts;
using LanguageExt.UnsafeValueAccess;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype;

/// <summary>
/// An immutable array
/// </summary>
/// <remarks>
/// Native array O(1) read performance.  Modifications require copying of the entire backing array to generate the
/// newly transformed collection. This will be expensive for large collections but potentially much faster than any
/// other data structure for smaller collections: use `Seq` if you need array-like performance and the ability to
/// transform larger collections efficiently.</remarks>
/// <remarks>
/// Two methods that don't suffer this fate are `Take` and `Skip` which will do splicing of the backing array, like
/// splicing of `Span` and `ReadOnlySpan`.  That makes those operations incredibly fast, but be aware that can mean
/// old data behind held longer than you may like (a space leak). If that's the case, use `Clone` to just take the
/// snapshot/view data you want so the old references can be collected by the GC.
/// </remarks>
/// <typeparam name="A">Value type</typeparam>
[Serializable]
[CollectionBuilder(typeof(Arr), nameof(Arr.create))]
[SkipLocalsInit]
public sealed class Arr<A> :
    IComparisonOperators<Arr<A>, Arr<A>, bool>,
    IAdditionOperators<Arr<A>, Arr<A>, Arr<A>>,
    Constructor<Arr<A>, ReadOnlySpan<A>>,
    IAdditiveIdentity<Arr<A>, Arr<A>>,
    TokenStream<Arr<A>, A>,
    IComparable<Arr<A>>,
    IEquatable<Arr<A>>,
    Monoid<Arr<A>>,
    IComparable,
    K<Arr, A>,
    IUnion
{
    /// <summary>
    /// Backing array
    /// </summary>
    internal readonly A[] Values;
    
    /// <summary>
    /// Start offset from the beginning of the backing array
    /// </summary>
    readonly int Start;

    /// <summary>
    /// Find the number of elements in the collection
    /// </summary>
    public int count;

    /// <summary>
    /// Cached hash code 
    /// </summary>
    int? hashCode;
    
    /// <summary>
    /// Cached empty collection
    /// </summary>
    static readonly Arr<A> empty = 
        new ([], 0, 0);
    
    /// <summary>
    /// Empty collection
    /// </summary>
    public static Arr<A> Empty => 
        empty;
    
    /// <summary>
    /// Find the number of elements in the collection
    /// </summary>
    public int Count => 
        count;
    
    /// <summary>
    /// Identity for an array (empty)
    /// </summary>
    [Pure]
    public static Arr<A> AdditiveIdentity => 
        Empty;

    /// <summary>
    /// Head lens
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, A> head =>
        LE.Lens<Arr<A>, A>.New(
        Get: la => la.Count == 0 ? throw new IndexOutOfRangeException() : la[0],
        Set: a => la => la.Count == 0 ? throw new IndexOutOfRangeException() : la.SetItemUnsafe(0, a));

    /// <summary>
    /// Head or none lens
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, LE.Option<A>> headOrNone =>
        LE.Lens<Arr<A>, LE.Option<A>>.New(
        Get: la => la.Count == 0 ? None : Some(la[0]),
        Set: a => la => la.Count == 0 || a.IsNone ? la : la.SetItemUnsafe(0, a.ValueUnsafe()!));

    /// <summary>
    /// Last lens
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, A> last =>
        LE.Lens<Arr<A>, A>.New(
        Get: la => la.Count == 0 ? throw new IndexOutOfRangeException() : la[^1],
        Set: a => la => la.Count == 0 ? throw new IndexOutOfRangeException() : la.SetItemUnsafe(la.Count - 1, a));

    /// <summary>
    /// Last or none lens
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, LE.Option<A>> lastOrNone =>
        LE.Lens<Arr<A>, LE.Option<A>>.New(
        Get: la => la.Count == 0 ? None : Some(la[^1]),
        Set: a => la => la.Count == 0 || a.IsNone ? la : la.SetItemUnsafe(la.Count - 1, a.ValueUnsafe()!));

    /// <summary>
    /// Item at index lens
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, A> item(int index) =>
        LE.Lens<Arr<A>, A>.New(
        Get: la => la.Count == 0 ? throw new IndexOutOfRangeException() : la[index],
        Set: a => la => la.Count == 0 ? throw new IndexOutOfRangeException() : la.SetItemUnsafe(index, a));

    /// <summary>
    /// Item or none at index lens
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, LE.Option<A>> itemOrNone(int index) =>
        LE.Lens<Arr<A>, LE.Option<A>>.New(
        Get: la => la.Count < index - 1 ? None : Some(la[index]),
        Set: a => la => la.Count < index - 1 || a.IsSome ? la : la.SetItemUnsafe(index, a.ValueUnsafe()!));

    /// <summary>
    /// Constructor
    /// </summary>
    internal Arr(A[] values, int start, int count)
    {
        Values = values;
        Start = start;
        this.count = count;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value">Value to construct from</param>
    /// <returns>A constructed array</returns>
    [Pure]
    public static Arr<A> Construct(in ReadOnlySpan<A> value) =>
        [.. value];

    public object Value => 
        TryGetValue(out var h, out var t) 
            ? new Cons<Arr, ArrState, A>(h, t) 
            : Nil.Obj;

    public bool HasValue =>
        true;

    /// <summary>
    /// Is the collection empty
    /// </summary>
    public bool IsEmpty =>
        false;

    /// <summary>
    /// Take all items other than the first
    /// </summary>
    /// <remarks>
    /// Equivalent to `Slice(1, Count - 1)`
    /// </remarks>
    public Arr<A> Tail =>
        Slice(1, Count - 1);

    /// <summary>
    /// Take all items other than the last
    /// </summary>
    /// <remarks>
    /// Equivalent to `Slice(0, length - 1)`
    /// </remarks>
    public Arr<A> Init =>
        Slice(0, Count - 1);

    /// <summary>
    /// Read the element at the index provided.
    /// </summary>
    /// <remarks>If the index is out of range, the result is `None`</remarks>
    /// <param name="index">Index of the element to read</param>
    /// <returns>Optional element value</returns>
    public LE.Option<A> At(Index index) =>
        (index.IsFromEnd, index.Value) switch
        {
            (false, var ix)        when ix < Count  => Values[Start         + ix],
            (true, var ix and > 0) when ix <= Count => Values[Start + Count - ix],
            _                                       => default
        };

    /// <summary>
    /// Get a readonly reference to the element at the index provided.
    /// </summary>
    /// <param name="index">Index of the element to read</param>
    /// <returns>Optional element value</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref readonly  A AtRef(int index) => 
        ref Values[Start + index];
    
    /// <summary>
    /// Indexer
    /// </summary>
    /// <remarks>
    /// Use `At` for a safe, non-exception throwing alternative.
    /// </remarks>
    /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of the range of the structure. Use `At`
    /// for a safe, non-exception throwing alternative.</exception>
    public A this[Index index] =>
        At(index).IfNone(() => throw new IndexOutOfRangeException());
    
    /// <summary>
    /// Test if the collection is empty
    /// </summary>
    /// <param name="nil">Nil structure</param>
    /// <returns>`true` if empty, `false` otherwise</returns>
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return Count == 0;
    }

    /// <summary>
    /// If the collection has elements, return the head element and an iterator that allows consumption of
    /// remaining elements in sequence.
    /// </summary>
    /// <param name="head">Head element</param>
    /// <param name="tail">Tail iterator</param>
    /// <returns>`true` if elements exist, `false` otherwise</returns>
    // ReSharper disable once ParameterHidesMember
    public bool TryGetValue(out A head, out Iterator<Arr, ArrState, A> tail)
    {
        Debug.Assert(Count > 0);
        head = Values[Start];
        tail = default!;
        return true;
    }

    /// <summary>
    /// If the collection has elements, return the head element and an iterator that allows consumption of
    /// remaining elements in sequence.
    /// </summary>
    /// <param name="cons">Head and tail element</param>
    /// <returns>`true` if elements exist, `false` otherwise</returns>
    [Pure]
    public bool TryGetValue(out Cons<Arr, ArrState, A> cons)
    {
        if (TryGetValue(out var h, out var t))
        {
            cons = new Cons<Arr, ArrState, A>(h, t);
            return true;
        }
        else
        {
            cons = default;
            return false;
        }
    }

    /// <summary>
    /// Create a readonly span of this array.  This doesn't do any copying, so it is very fast.   
    /// </summary>
    /// <returns>A read-only span of values</returns>
    public ReadOnlySpan<A> AsSpan() =>
        new (Values, Start, Count); 

    /// <summary>
    /// Create a readonly sub-span of this array.   
    /// </summary>
    /// <remarks>
    /// This doesn't do any copying, so is very fast, but be aware that the GC still actively tracks any items outside
    /// the spliced subsection, which you may consider to be wasteful. If so, consider using:
    /// <code>
    ///     Slice(skip).Copy().AsSpan()
    /// </code>
    /// Which will create a new array with just the spliced subsection and will allow any references outside the
    /// spliced subsection to be garbage-collected.
    /// </remarks>
    /// <param name="skip">Offset from the beginning of the array</param>
    /// <returns>A read-only span of values</returns>
    public ReadOnlySpan<A> AsSpan(int skip) =>
        skip >= Count
            ? ReadOnlySpan<A>.Empty
            : new (Values, Start + skip, Count - skip);

    /// <summary>
    /// Create a readonly sub-span of this array.   
    /// </summary>
    /// <remarks>
    /// This doesn't do any copying, so is very fast, but be aware that the GC still actively tracks any items outside
    /// the spliced subsection, which you may consider to be wasteful. If so, consider using:
    /// <code>
    ///     Slice(skip, take).Copy().AsSpan()
    /// </code>
    /// Which will create a new array with just the spliced subsection and will allow any references outside the
    /// spliced subsection to be garbage-collected.
    /// </remarks>
    /// <param name="skip">Offset from the beginning of the array</param>
    /// <param name="take">The number of items to take. This will be clamped to the maximum number of items available</param>
    /// <returns>A read-only span of values</returns>
    public ReadOnlySpan<A> AsSpan(int skip, int take) =>
        Count - skip <= 0 || take <= 0
            ? ReadOnlySpan<A>.Empty
            :  new (Values, Start + skip, Math.Min(take, Count - skip));

    /// <summary>
    /// Create a subarray of this array.   
    /// </summary>
    /// <remarks>
    /// This doesn't do any copying, so is very fast, but be aware that the GC still actively tracks any items outside
    /// the spliced subsection, which you may consider to be wasteful. If so, consider using:
    /// <code>
    ///     Slice(skip).Copy()
    /// </code>
    /// Which will create a new array with just the spliced subsection and will allow any references outside the
    /// spliced subsection to be garbage-collected.
    /// </remarks>
    /// <param name="skip">Offset from the beginning of the array</param>
    /// <returns>Subset of this array</returns>
    public Arr<A> Slice(int skip) =>
        (Count - skip) switch
        {
            < 1   => Empty,
            var n => new Arr<A>(Values, Start + skip, n)
        };

    /// <summary>
    /// Create a subarray of this array.  This doesn't do any copying, so is very fast, but be aware that any items
    /// outside the splice are still active.   
    /// </summary>
    /// <remarks>
    /// This doesn't do any copying, so is very fast, but be aware that the GC still actively tracks any items outside
    /// the spliced subsection, which you may consider to be wasteful. If so, consider using:
    /// <code>
    ///     Slice(skip, take).Copy()
    /// </code>
    /// Which will create a new array with just the spliced subsection and will allow any references outside the
    /// spliced subsection to be garbage-collected.
    /// </remarks>
    /// <param name="skip">Offset from the beginning of the array</param>
    /// <param name="take">The number of items to take. This will be clamped to the maximum number of items available</param>
    /// <returns>Subset of this array</returns>
    public Arr<A> Slice(int skip, int take) =>
        take switch
        {
            < 1 => Empty,
            var t => (Count - skip) switch
                     {
                         < 1   => Empty,
                         var n => new Arr<A>(Values, Start + skip, Math.Min(t, n))
                     }
        };

    /// <summary>
    /// Set an item at the specified index
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which in which the item being set has changed.
    /// That is 'okay' for certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure
    /// that can handle an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    public LE.Option<Arr<A>> SetItem(Index index, A val)
    {
        var offset = index.GetOffset(Count);
        if(offset < 0 || offset >= Count) return None;
        var ovalues = AsSpan();
        var nvalues = new A[Count - Start];
        nvalues[offset] = val;
        ovalues.CopyTo(nvalues);
        return new Arr<A>(nvalues, 0, Count);
    }

    /// <summary>
    /// Set an item at the specified index
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which in which the item being set has changed.
    /// That is 'okay' for certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure
    /// that can handle an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> SetItemUnsafe(Index index, A value) =>
        SetItem(index, value).IfNone(() => throw new IndexOutOfRangeException());            

    /// <summary>
    /// Add an item to the end of the array
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element larger. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> Add(in A value)
    {
        var span   = AsSpan();
        var narray = new A[Count + 1];
        var nspan  = narray.AsSpan();
        span.CopyTo(nspan);
        narray[^1] = value;
        return new Arr<A>(narray, 0, Count + 1);
    }

    /// <summary>
    /// Prepend an item to the beginning of the array
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element larger. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> Cons(in A value)
    {
        var span   = AsSpan();
        var narray = new A[Count + 1];
        var nspan  = narray.AsSpan();
        span.CopyTo(nspan[1..]);
        narray[0] = value;
        return new Arr<A>(narray, 0, Count + 1);
    }

    /// <summary>
    /// Concatenate this collection the collection provided
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> AddRange(in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var lcount = Count;
        var rcount = range.Length;
        var ncount = lcount + rcount;
        var lspan  = AsSpan();
        var narray = new A[ncount];
        var nlspan = narray.AsSpan(0, lcount);
        var nrspan = narray.AsSpan(lcount, rcount);
        
        lspan.CopyTo(nlspan);
        range.CopyTo(nrspan);
        
        return new Arr<A>(narray, 0, ncount);
    }

    /// <summary>
    /// Concatenate this collection the collection provided
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> AddRange(IEnumerable<A> range) =>
        AddRange((ReadOnlySpan<A>)[..range]);

    /// <summary>
    /// Concatenate this collection the collection provided
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> AddRange(Arr<A> range) =>
        Count switch
        {
            0 => range,
            _ => AddRange(range.AsSpan())
        };

    /// <summary>
    /// Concatenate this collection the collection provided
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> AddRange<T>(in K<T, A> range)
        where T : Iterable<T> =>
        AddRange(range.AsSpan());

    /// <summary>
    /// Concatenate the provided collection with this collection (the provided collection is prepended to this one) 
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> ConsRange(in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var lcount = Count;
        var rcount = range.Length;
        var ncount = lcount + rcount;
        var lspan  = AsSpan();
        var narray = new A[ncount];
        var nlspan = narray.AsSpan(0, rcount);
        var nrspan = narray.AsSpan(rcount, lcount);
        
        range.CopyTo(nlspan);
        lspan.CopyTo(nrspan);
        
        return new Arr<A>(narray, 0, ncount);
    }
        /// <summary>
    /// Concatenate the provided collection with this collection (the provided collection is prepended to this one) 
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> ConsRange(IEnumerable<A> range) =>
        ConsRange((ReadOnlySpan<A>)[..range]);
    
    /// <summary>
    /// Concatenate the provided collection with this collection (the provided collection is prepended to this one) 
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> ConsRange(Arr<A> range) =>
        Count switch
        {
            0 => range,
            _ => ConsRange(range.AsSpan())
        };

    /// <summary>
    /// Concatenate the provided collection with this collection (the provided collection is prepended to this one) 
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public Arr<A> ConsRange<T>(in K<T, A> range)
        where T : Iterable<T> =>
        ConsRange(range.AsSpan());

    /// <summary>
    /// Insert an element at the specified index
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element larger. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.
    /// </remarks>
    /// <param name="index">Index to insert at</param>
    /// <param name="value">Element value to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    [Pure]
    public LE.Option<Arr<A>> Insert(Index index, in A value)
    {
        var offset = index.GetOffset(Count);
        
        if (offset == 0)
        {
            return Cons(in value);
        }
        else if (offset == Count)
        {
            return Add(in value);
        }
        else if(offset > 0 && offset < Count)
        {
            var narray  = new A[Count + 1];
            var nspan   = narray.AsSpan();
            var span    = AsSpan();
            var roffset = offset + 1;
            
            span[..offset].CopyTo(nspan[..offset]);
            narray[offset] = value;
            span[offset..].CopyTo(nspan[roffset..]);
            
            return new Arr<A>(narray, 0, narray.Length);
        }
        else
        {
            return LE.Option<Arr<A>>.None;
        }
    }

    /// <summary>
    /// Insert an element at the specified index
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element larger. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.
    /// </remarks>
    /// <param name="index">Index to insert at</param>
    /// <param name="value">Element value to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    public Arr<A> InsertUnsafe(Index index, in A value) =>
        Insert(index, in value).IfNone(() => throw new IndexOutOfRangeException());
    
    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public LE.Option<Arr<A>> InsertRange(Index index, Arr<A> range)
    {
        var offset = index.GetOffset(Count);
        return offset >= 0 && offset <= Count
                   ? InsertRange(index, range.AsSpan())
                   : None;
    }

    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    public Arr<A> InsertRangeUnsafe(Index index, Arr<A> range) =>
        InsertRange(index, range).IfNone(() => throw new IndexOutOfRangeException());

    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public LE.Option<Arr<A>> InsertRange<T>(Index index, in K<T, A> range)
        where T : Iterable<T>
    {
        var offset = index.GetOffset(Count);
        return offset >= 0 && offset <= Count
                   ? InsertRange(index, range.AsSpan())
                   : None;
    }

    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    public Arr<A> InsertRangeUnsafe<T>(Index index, in K<T, A> range) 
        where T : Iterable<T> =>
        InsertRange(index, range).IfNone(() => throw new IndexOutOfRangeException());

    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public LE.Option<Arr<A>> InsertRange(Index index, IEnumerable<A> range) =>
        InsertRange(index, (ReadOnlySpan<A>)[.. range]);

    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    public Arr<A> InsertRangeUnsafe(Index index, IEnumerable<A> range) =>
        InsertRange(index, range).IfNone(() => throw new IndexOutOfRangeException());

    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    [Pure]
    public LE.Option<Arr<A>> InsertRange(Index index, in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var offset = index.GetOffset(Count);
        if (offset == 0)
        {
            return ConsRange(in range);
        }
        else if (offset == Count)
        {
            return AddRange(in range);
        }
        else if(offset > 0 && offset < Count)
        {
            var narray  = new A[Count + range.Length];
            var nspan   = narray.AsSpan();
            var span    = AsSpan();
            var roffset = offset + range.Length;
            
            span.CopyTo(nspan[..offset]);
            range.CopyTo(nspan[offset..roffset]);
            span[offset..].CopyTo(nspan[roffset..]);
            
            return new Arr<A>(narray, 0, narray.Length);
        }
        else
        {
            return LE.Option<Arr<A>>.None;
        }
    }
    
    /// <summary>
    /// Insert a range of elements at the specified index
    /// </summary>
    /// <param name="index">Index to insert at</param>
    /// <param name="range">Range of elements to insert</param>
    /// <returns>An updated `Arr` or `None` if the index was out-of-bounds</returns>
    /// <remarks>NOTE: This needs to create a whole new backing array which is the size of this collection and the range
    /// provided. The two collections are then copied to the new backing array. That is 'okay' for certain scenarios,
    /// but it is inefficient if done regularly.  Consider using a data-structure that can handle an expanding set as
    /// its core offering, like `Seq` or `Lst`.</remarks>
    public Arr<A> InsertRangeUnsafe(Index index, in ReadOnlySpan<A> range) =>
        InsertRange(index, range).IfNone(() => throw new IndexOutOfRangeException());

    /// <summary>
    /// Remove the head item (if one exists)
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element smaller. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    /// <returns>This collection with its first item removed.  If the collection is empty, an empty collection is
    /// returned.</returns>
    [Pure]
    public Arr<A> RemoveAtHead()
    {
        if (Count == 1) return Empty; 
        var narray = new A[Count - 1];
        var nspan  = narray.AsSpan();
        var span   = AsSpan();
        span[1..].CopyTo(nspan);
        return new Arr<A>(narray, 0, narray.Length);
    }

    /// <summary>
    /// Remove the last item (if one exists)
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element smaller. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    /// <returns>This collection with its last item removed.  If the collection is empty, an empty collection is
    /// returned.</returns>
    [Pure]
    public Arr<A> RemoveAtLast()
    {
        if (Count == 1) return Empty; 
        var narray = new A[Count - 1];
        var nspan  = narray.AsSpan();
        var span   = AsSpan();
        span[..^1].CopyTo(nspan);
        return new Arr<A>(narray, 0, narray.Length);
    }

    /// <summary>
    /// Remove the item at the index provided.
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is one element smaller. That is 'okay' for
    /// certain scenarios, but it is inefficient if done regularly.  Consider using a data-structure that can handle
    /// an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    /// <returns>Returns this collection with the item, at the index provided, removed.</returns>
    [Pure]
    public Arr<A> RemoveAt(Index index)
    {
        var offset = index.GetOffset(Count);

        if (offset == 0)
        {
            return RemoveAtHead();
        }
        else if (offset == Count)
        {
            return RemoveAtLast();
        }
        else if(offset > 0 && offset < Count)
        {
            var narray = new A[Count - 1];
            var nspan  = narray.AsSpan();
            var span   = AsSpan();
            
            span[..offset].CopyTo(nspan);
            span[(offset+1)..].CopyTo(nspan[offset..]);
            
            return new Arr<A>(narray, 0, narray.Length);
        }
        else
        {
            return this;
        }
    }

    /// <summary>
    /// Remove all items at the indices provided.
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is `n` elements smaller (where `n` is the
    /// number of indices provided). That is 'okay' for certain scenarios, but it is inefficient if done regularly.
    /// Consider using a data-structure that can handle an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    /// <returns>Returns this collection with the items, at the indices provided, removed.</returns>
    /// <param name="indices"></param>
    /// <returns></returns>
    [Pure]
    public Arr<A> RemoveAt(ReadOnlySpan<Index> indices)
    {
        if (indices.IsEmpty) return this;
        HashSet<int> set = [];
        foreach (var ix in indices)
        {
            var offset = ix.GetOffset(Count);
            if (offset >= 0 && offset < Count)
            {
                set.Add(offset);
            }
        }
        
        var w = LE.ArrayWriter<A>.Init(Count - set.Count);
        var i = 0;
        foreach (var x in Values)
        {
            if (!set.Contains(i))
            {
                LE.ArrayWriter<A>.Add(ref w, in x);
            }
            i++;
        }

        return w.ToArr();
    }

    /// <summary>
    /// Remove a range of items
    /// </summary>
    /// <remarks>NOTE: This needs to create a whole new backing array which is `n` elements smaller (where `n` is the
    /// size of the range provided). That is 'okay' for certain scenarios, but it is inefficient if done regularly.
    /// Consider using a data-structure that can handle an expanding set as its core offering, like `Seq` or `Lst`.</remarks>
    /// <returns>Returns this collection with the items within the range provided, removed.</returns>
    [Pure]
    public Arr<A> RemoveRange(in Range range)
    {
        var begin = range.Start.GetOffset(Count);
        var end   = range.End.GetOffset(Count);
        (begin, end) = begin > end 
                           ? (end, begin) 
                           : (begin, end);

        var narray = new A[Count - (end - begin)];
        var nspan  = narray.AsSpan();
        var span   = AsSpan();
        
        span[..begin].CopyTo(nspan);
        span[end..].CopyTo(nspan[begin..]);

        return [.. span];
    }

    /// <summary>
    /// Reverse the order of the items in the collection
    /// </summary>
    [Pure]
    public Arr<A> Reverse()
    {
        var span   = AsSpan();
        var narray = new A[Count];
        var nspan  = narray.AsSpan();

        span.CopyTo(nspan);
        nspan.Reverse();

        return [.. nspan];
    }
    
    /// <summary>
    /// Operations like `Take` or `Skip` can result in a lot of unused backing buffers, so this method
    /// allows you to make a copy of just the active buffer and create a new instance with it.  The old
    /// collection can then be dereferenced, allowing the GC to collect it. 
    /// </summary>
    /// <returns>A copy of this instance, with any fat trimmed</returns>
    [Pure]
    public Arr<A> Copy() =>
        new (AsSpan().ToArray(), 0, Count);
    
    /// <summary>
    /// Functor map: projects each element of this collection to a new value
    /// </summary>
    /// <param name="f">Projection function</param>
    /// <typeparam name="B">Resulting value-type</typeparam>
    /// <returns>A new collection</returns>
    [Pure]
    public Arr<B> Map<B>(Func<A, B> f)
    {
        var ma = this;
        var w  = LE.ArrayWriter<B>.Init(Count);
        var ts = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref ts, out var x))
        {
            w.Add(f(x));
        }
        return w.ToArr();
    }

    /// <summary>
    /// Monadic bind: projects each element of this collection to a new collection and concatenates the results
    /// </summary>
    /// <param name="f">Projection function</param>
    /// <typeparam name="B">Resulting value-type</typeparam>
    /// <returns>A new collection</returns>
    [Pure]
    public Arr<B> Bind<B>(Func<A, Arr<B>> f)
    {
        var ma = this;
        var w  = LE.ArrayWriter<B>.Init();
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref sa, out var x))
        {
            var mb = f(x);
            var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, B>(mb);
            while (IterableMutable.step<Arr, ArrState, ArrStateRef, B>(mb, ref sb, out var y))
            {
                w.Add(y);
            }
        }
        return w.ToArr();
    }

    /// <summary>
    /// Monadic bind: projects each element of this collection to a new collection and concatenates the results
    /// </summary>
    /// <param name="f">Projection function</param>
    /// <typeparam name="B">Resulting value-type</typeparam>
    /// <returns>A new collection</returns>
    [Pure]
    public Arr<B> Bind<B>(Func<A, K<Arr, B>> f)
    {
        var ma = this;
        var w  = LE.ArrayWriter<B>.Init();
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref sa, out var x))
        {
            var mb = f(x);
            var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, B>(mb);
            while (IterableMutable.step<Arr, ArrState, ArrStateRef, B>(mb, ref sb, out var y))
            {
                w.Add(y);
            }
        }
        return w.ToArr();
    }
    
    /// <summary>
    /// Filter: projects each value in the structure to a boolean and returns only those values for which the boolean
    /// is `true`.
    /// </summary>
    [Pure]
    public Arr<A> Filter(Func<A, bool> f)
    {
        var ma     = this;
        var writer = LE.ArrayWriter<A>.Init();
        var ts     = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ma);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ma, ref ts, out var x))
        {
            if (f(x))
            {
                writer.Add(x);
            }
        }
        return writer.ToArr();
    }
    
    /// <summary>
    /// If this structure is empty, return the second structure; otherwise return this structure.
    /// </summary>
    /// <param name="tb">Second structure to return if the first one is empty</param>
    /// <returns>First argument to 'succeed', `this` or `tb`</returns>
    [Pure]
    public Arr<A> Choose(K<Arr, A> tb) =>
        +tb;

    public bool Equals<EqA>(K<Arr, A>? rhs)
        where EqA : Eq<A>
    {
        if (rhs is not Arr<A>) return false;
        if (Count != rhs.Count) return false;

        var ta = this;
        var tb = rhs;
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ta);
        var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(tb);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ta, ref sa, out var x) &&
               IterableMutable.step<Arr, ArrState, ArrStateRef, A>(tb, ref sb, out var y))
        {
            if (!EqA.Equals(x, y)) return false;
        }
        
        return true;
    }
    
    public int CompareTo<OrdA>(K<Arr, A>? rhs)
        where OrdA : Ord<A>
    {
        if (rhs is null || Count > rhs.Count) return 1;
        if (Count < rhs.Count) return -1;

        var ta = this;
        var tb = rhs;
        var sa = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(ta);
        var sb = IterableMutable.setup<Arr, ArrState, ArrStateRef, A>(tb);

        while (IterableMutable.step<Arr, ArrState, ArrStateRef, A>(ta, ref sa, out var x) &&
               IterableMutable.step<Arr, ArrState, ArrStateRef, A>(tb, ref sb, out var y))
        {
            switch (OrdA.Compare(x, y))
            {
                case <0: return -1;
                case >0: return 1;
            }
        }
        return 0;
    }
    
    /// <summary>
    /// Part of the monoid category: equivalent to concatenation.
    /// </summary>
    /// <param name="rhs">Collection to append</param>
    /// <returns>Combined collection</returns>
    [Pure]
    public Arr<A> Combine(Arr<A> rhs) =>
        AddRange(rhs);
    
    /// <summary>
    /// Part of the monoid category: equivalent to concatenation.
    /// </summary>
    /// <param name="rhs">Collection to append</param>
    /// <returns>Combined collection</returns>
    [Pure]
    public Arr<A> Combine(K<Arr, A> rhs) =>
        AddRange(rhs);

    /// <summary>
    /// Return a mutable ref-struct enumerable that can be used for rapid iteration of the items in
    /// this collection.   
    /// </summary>
    /// <remarks>
    /// Note, it cannot be used with `yield` or `async/await`, If you need to enumerate this and either
    /// `yield` or `await` then use `GetEnumerator()`.
    /// </remarks>
    [Pure]
    public IterableMutableEnumerable<Arr, ArrState, ArrStateRef, A> reference =>
        new (this);

    /// <summary>
    /// Return a struct enumerable that can be used for rapid iteration of the items in this collection.   
    /// </summary>
    /// <remarks>
    /// Note, this is slower than `.reference` but it can be used with  `yield` or `async/await`, If you don't need to
    /// enumerate this and either `yield` or `await` then use `reference`.
    /// </remarks>
    [Pure]
    public IterableImmutableEnumerable<Arr, ArrState, A> nonref =>
        new (this);

    /// <summary>
    /// Return an enumerator that can be used for rapid iteration of the items in this collection.   
    /// </summary>
    public IterableImmutableEnumerator<Arr, ArrState, A> GetEnumerator() =>
        new (this);

    /// <summary>
    /// Implicit conversion operator from a system array. 
    /// </summary>
    /// <remarks>Note, this must copy to prevent mutation of the underlying array.</remarks>
    /// <param name="xs"></param>
    /// <returns></returns>
    [Pure]
    public static implicit operator Arr<A>(A[] xs) =>
        xs switch
        {
            [] => Empty,
            _  => new Arr<A>(CopyArray(xs), 0, xs.Length)
        };

    /// <summary>
    /// Equality operator
    /// </summary>
    /// <param name="obj">Right-hand side of the equality expression</param>
    /// <returns>`true` if `rhs` is equal to `this`</returns>
    [Pure]  
    public override bool Equals(object? obj) =>
        obj is Arr<A> @as && Equals(@as);

    /// <summary>
    /// Equality operator
    /// </summary>
    /// <param name="rhs">Right-hand side of the equality expression</param>
    /// <returns>`true` if `rhs` is equal to `this`</returns>
    [Pure]
    public bool Equals(K<Arr, A>? rhs) =>
        Equals<EqDefault<A>>(rhs);

    /// <summary>
    /// Equality operator
    /// </summary>
    /// <param name="rhs">Right-hand side of the equality expression</param>
    /// <returns>`true` if `rhs` is equal to `this`</returns>
    [Pure]
    public bool Equals(Arr<A>? rhs) =>
        Equals<EqDefault<A>>(rhs);

    /// <summary>
    /// Ordering operator
    /// </summary>
    /// <param name="rhs">Right-hand side of the comparison expression</param>
    /// <returns>`0` if the two collections are equal. `-1` if `rhs` is greater than `this`. `1` if `rhs` is less
    /// than `this`</returns>
    [Pure]
    public int CompareTo(object? rhs) =>
        rhs is Arr<A> t ? CompareTo(t) : 1;

    /// <summary>
    /// Ordering operator
    /// </summary>
    /// <param name="rhs">Right-hand side of the comparison expression</param>
    /// <returns>`0` if the two collections are equal. `-1` if `rhs` is greater than `this`. `1` if `rhs` is less
    /// than `this`</returns>
    [Pure]
    public int CompareTo(Arr<A>? rhs) =>
        CompareTo<OrdDefault<A>>(rhs);

    /// <summary>
    /// Ordering operator
    /// </summary>
    /// <param name="rhs">Right-hand side of the comparison expression</param>
    /// <returns>`0` if the two collections are equal. `-1` if `rhs` is greater than `this`. `1` if `rhs` is less
    /// than `this`</returns>
    [Pure]
    public int CompareTo(K<Arr, A>? rhs) =>
        CompareTo<OrdDefault<A>>(rhs);
    
    /// <summary>
    /// Get the hash code of the collection
    /// </summary>
    /// <remarks>
    /// For multi-item collections the hash-code is calculated using the Fowler–Noll–Vo hash function and then cached,
    /// so there is no need to perform (the potentially expensive) operation multiple times.  For singleton collections
    /// the singleton element has `GetHashCode()` called on it: no caching is performed. And for empty collections the
    /// constant starting value is returned, which is equivalent to a cached value.
    /// </remarks>
    /// <returns>The calculated hash code for every element in the collection using the Fowler–Noll–Vo hash function</returns>
    [Pure]
    public override int GetHashCode() =>
        CalculateHashCode();

    [Pure]
    public static bool operator ==(Arr<A>? lhs, Arr<A>? rhs) =>
        lhs?.Equals(rhs) ?? rhs is null;

    [Pure]
    public static bool operator !=(Arr<A>? lhs, Arr<A>? rhs) =>
        !(lhs == rhs);

    [Pure]
    public static bool operator >(Arr<A> left, Arr<A> right) =>
        left.CompareTo(right) > 0;
    
    [Pure]
    public static bool operator >=(Arr<A> left, Arr<A> right) =>
        left.CompareTo(right) >= 0;
    
    [Pure]
    public static bool operator <(Arr<A> left, Arr<A> right) =>
        left.CompareTo(right) < 0;
    
    [Pure]
    public static bool operator <=(Arr<A> left, Arr<A> right) =>
        left.CompareTo(right) <= 0;
    
    [Pure]
    public static Arr<A> operator +(Arr<A> left, Arr<A> right) =>
        left.AddRange(right);

    [Pure]
    public static implicit operator Arr<A>(Nil _) =>
        Empty;

    [Pure]
    public static implicit operator Arr<A>(LE.Unit _) =>
        Empty;
    
    /// <summary>
    /// Fast copy of the collection
    /// </summary>
    static A[] CopyArray(A[] array)
    {
        var span   = array.AsSpan();
        var narray = new A[array.Length];
        var nspan  = narray.AsSpan();
        span.CopyTo(nspan);
        return narray;
    }
    
    int CalculateHashCode(int offsetBasis = -2128831035)
    {
        var hash = offsetBasis;
        const int prime = 16777619;

        unchecked
        {
            var xs = Values;
            for (var current = Start; current < Start + Count; current++)
            {
                var x = xs[current];
                hash = ((x?.GetHashCode() ?? 0) ^ hash) * prime;
            }
            return hash;
        }        
    }
    
    /*  TODO
     
    /// <summary>
    /// Lens map
    /// </summary>
    [Pure]
    public static LE.Lens<Arr<A>, Arr<B>> map<B>(LE.Lens<A, B> lens) =>
        LE.Lens<Arr<A>, Arr<B>>.New(
        Get: la => la.Map(lens.Get),
        Set: lb => la => la.Zip(lb).Map(ab => lens.Set(ab.Item2, ab.Item1)).ToArr());
        */
    
    /*
     TODO
     
    /// <summary>
    /// Map each element of a structure to an action, evaluate these actions from
    /// left to right, and collect the results.
    /// </summary>
    /// <param name="f"></param>
    /// <typeparam name="F">Applicative functor trait</typeparam>
    /// <typeparam name="B">Bound value (output)</typeparam>
    [Pure]
    public K<F, Arr<B>> Traverse<F, B>(Func<A, K<F, B>> f) 
        where F : Applicative<F> =>
        F.Map(x => x.As(), Traversable.traverse(f, this));
    
    /// <summary>
    /// Map each element of a structure to an action, evaluate these actions from
    /// left to right, and collect the results.
    /// </summary>
    /// <param name="f"></param>
    /// <typeparam name="M">Monad trait</typeparam>
    /// <typeparam name="B">Bound value (output)</typeparam>
    [Pure]
    public K<M, Arr<B>> TraverseM<M, B>(Func<A, K<M, B>> f) 
        where M : Monad<M> =>
        M.Map(x => +x, Traversable.traverseM(f, this));
        */

    /*
    readonly A[]? value;
    internal readonly long start;
    readonly long length;
    readonly L.Atom<int>? hashCode;

    /// <summary>
    /// Ctor
    /// </summary>
    public Arr(IEnumerable<A> initial)
    {
        hashCode = Atom(0);
        value = [.. initial];
        start = 0;
        length = value.Length;
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public Arr(ReadOnlySpan<A> initial)
    {
        hashCode = Atom(0);
        value = [.. initial];
        start = 0;
        length = value.Length;
    }

    /// <summary>
    /// Ctor
    /// </summary>
    internal Arr(A[] value)
    {
        hashCode = Atom(0);
        this.value = value;
        start = 0;
        length = value.Length;
    }

    /// <summary>
    /// Ctor
    /// </summary>
    internal Arr(A[] value, long start, long length)
    {
        if(start < 0) throw new ArgumentOutOfRangeException(nameof(start));
        if(start + length > value.Length) throw new ArgumentOutOfRangeException(nameof(length));
        hashCode = Atom(0);
        this.value = value;
        this.start = start;
        this.length = length;
    }*/

    static bool TokenStream<Arr<A>, A>.IsTab(A token) =>
        false;

    static bool TokenStream<Arr<A>, A>.IsNewline(A token) => 
        false;

    static ReadOnlySpan<char> TokenStream<Arr<A>, A>.TokenToString(A token) => 
        (token?.ToString() ?? "").AsSpan() ;

    static Arr<A> TokenStream<Arr<A>, A>.TokenToChunk(in A token) => 
        Arr.singleton(token);

    static Arr<A> TokenStream<Arr<A>, A>.TokensToChunk(in ReadOnlySpan<A> token) => 
        [..token];

    static ReadOnlySpan<A> TokenStream<Arr<A>, A>.ChunkToTokens(in Arr<A> tokens) => 
        tokens.AsSpan();

    static int TokenStream<Arr<A>, A>.ChunkLength(in Arr<A> tokens) => 
        tokens.Count;

    static bool TokenStream<Arr<A>, A>.Take1(in Arr<A> stream, out A head, out Arr<A> tail)
    {
        var s = stream;
        if (s.IsEmpty)
        {
            head = default!;
            tail = stream;
            return false;
        }
        else
        {
            head = s[0];
            tail = s.Tail;
            return true;
        }
    }

    static bool TokenStream<Arr<A>, A>.Take(int amount, in Arr<A> stream, out Arr<A> head, out Arr<A> tail)
    {
        // If the requested length `amount` is 0 (or less), `false` should
        // not be returned, instead `true` and `(out Empty, out stream)` should be returned.
        if (amount <= 0)
        {
            head = [];
            tail = stream;
            return true;
        }

        // Take
        head = stream[..amount];
        tail = stream[amount..];
        
        // If the requested length is greater than 0 and the stream is
        // empty, `false` should be returned indicating end-of-input.
        return stream.Count > 0;
    }

    static void TokenStream<Arr<A>, A>.TakeWhile(Func<A, bool> predicate, in Arr<A> stream, out Arr<A> head, out Arr<A> tail)
    {
        var span   = stream.AsSpan();
        var length = span.Length;
        
        for(var current = 0; current < length; current++)
        {
            if (!predicate(span[current]))
            {
                head = stream[..current];
                tail = stream[current..];
                return;
            }
        }
        head = stream;
        tail = [];
    }
}
