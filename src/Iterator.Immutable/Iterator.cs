// ReSharper disable ParameterHidesMember
// ReSharper disable NotAccessedField.Local
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

/*
[SkipLocalsInit]
public ref struct IteratorMutable<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    // MUST MATCH THE FIELDS IN Iterator<T, TS, A>
    public IteratorTag tag;
    public A head;
    public K<T, A> ta;
    public Func<Iterator<T, IS, A>>? lazy;
    public VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public IS space;
}
*/

[Union]
[SkipLocalsInit]
public readonly struct Iterator<T, IS, A> : IUnion, IIterator<Iterator<T, IS, A>, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    readonly IteratorFields<T, IS, A> fields;

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => fields.tag == IteratorTag.Empty;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in Nil nil) =>
        fields = new IteratorFields<T, IS, A>(in nil);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A one) =>
        fields = new IteratorFields<T, IS, A>(in one);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Func<Iterator<T, IS, A>> tail) =>
        fields = new IteratorFields<T, IS, A>(in head, tail);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Iterator<T, IS, A> tail) =>
        fields = new IteratorFields<T, IS, A>(in head, tail);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<T, IS, A>> lazy) =>
        fields = new IteratorFields<T, IS, A>(lazy);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, in K<T, A> source, in IS state) =>
        fields = new IteratorFields<T, IS, A>(in head, in source, in state);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<T, IS, A>> init, in A last) =>
        fields = new IteratorFields<T, IS, A>(init, in last);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Iterator<T, IS, A> init, in A last) =>
        fields = new IteratorFields<T, IS, A>(init, in last);
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return fields.tag == 0;
    }
    
    public Iterator<A> Lower
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get
        {
            var iter = this;
            return Unsafe.As<Iterator<T, IS, A>, Iterator<A>>(ref iter); 
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Cons<T, IS, A> cons)
    {
        if(TryGetValue(out var h, out var t))
        {
            cons = new Cons<T, IS, A>(in h, in t);
            return true;
        }
        else
        {
            cons = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out A head, out Iterator<T, IS, A> tail) =>
        fields.tag switch
        {
            IteratorTag.Iterable  => IterableCase(out head, out tail),
            IteratorTag.Singleton => SingletonCase(out head, out tail),
            IteratorTag.Cons      => ConsCase(out head, out tail),
            IteratorTag.Lazy      => LazyCase(out head, out tail),
            IteratorTag.Add       => AddCase(out head, out tail),
            _                     => EmptyCase(out head, out tail)
        };

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool IterableCase(out A head, out Iterator<T, IS, A> tail)
    {
        tail = this; // Copy
        head = fields.head;
        T.Next(in fields.ta!, ref Unsafe.As<Iterator<T, IS, A>, IteratorFieldsMutable<T, IS, A>>(ref tail));
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool EmptyCase(out A head, out Iterator<T, IS, A> tail)
    {
        head = default!;
        tail = default!;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool SingletonCase(out A head, out Iterator<T, IS, A> tail)
    {
        head = fields.head;
        tail = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool ConsCase(out A head, out Iterator<T, IS, A> tail)
    {
        head = fields.head;
        tail = fields.lazy!();
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool LazyCase(out A head, out Iterator<T, IS, A> tail)
    {
        return fields.lazy!().TryGetValue(out head, out tail);    
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool AddCase(out A head, out Iterator<T, IS, A> tail)
    {
        var first = fields.lazy!();
        if (first.TryGetValue(out head, out var nt))
        {
            tail = new Iterator<T, IS, A>(nt, fields.head);
        }
        else
        {
            head = fields.head;
            tail = default;
        }

        return true;
    }

    public bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => fields.tag is >= IteratorTag.Empty and < IteratorTag.MaxValue;
    }

    public object? Value =>
        TryGetValue(out Cons<T, IS, A> cons)
            ? cons
            : new Nil();
    
    public IteratorEnumerator<T, IS, A> GetEnumerator() => 
        new(this);

    public static Iterator<T, IS, A> operator +(in A head, in Iterator<T, IS, A> tail) =>
        new (head, tail);

    public static Iterator<T, IS, A> operator +(in Iterator<T, IS, A> first, in A next) =>
        new (first, next);
    
    public static implicit operator Iterator<T, IS, A> (Nil nil) =>
        default;

    public static implicit operator Iterator<A>(Iterator<T, IS, A> iter) =>
        Unsafe.As<Iterator<T, IS, A>, Iterator<A>>(ref iter);
    
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    // LINQ operators
    // 

    /// <summary>
    /// Projects each element of a range into a new form.
    /// </summary>
    public Iterator<T, IS, B> Select<B>(Func<A, B> f)
    {
        switch (fields.tag)
        {
            case IteratorTag.Singleton:
                return new Iterator<T, IS, B>(f(fields.head));

            case IteratorTag.Cons:
            {
                var tail = fields.lazy ?? (() => default);
                return new Iterator<T, IS, B>(f(fields.head), () => tail().Select(f));
            }

            case IteratorTag.Lazy:
            {
                var iter = fields.lazy ?? (() => default);
                return new Iterator<T, IS, B>(() => iter().Select(f));
            }

            case IteratorTag.Iterable:
            {
                var s = this;
                var t = s;        // Copy
                var h = s.fields.head;
                T.Next(in s.fields.ta!, ref Unsafe.As<Iterator<T, IS, A>, IteratorFieldsMutable<T, IS, A>>(ref t));
                return new Iterator<T, IS, B>(f(h), () => t.Select(f));
            }

            case IteratorTag.Add:
            {
                var tail = fields.lazy ?? (() => default);
                return new Iterator<T, IS, B>(() => tail().Select(f), f(fields.head));
            }
            
            default:
                return default;
        }
    }    
}
