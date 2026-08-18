// ReSharper disable ParameterHidesMember
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[Union]
[SkipLocalsInit]
public readonly struct Iterator<A> : IUnion, IIterator<Iterator<A>, A>, K<Iterator, A>
{
    readonly IteratorFields<A> fields;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in Nil nil) =>
        fields = new IteratorFields<A>(in nil);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A one) =>
        fields = new IteratorFields<A>(in one);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Func<Iterator<A>> tail) =>
        fields = new IteratorFields<A>(in head, tail);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Iterator<A> tail) =>
        fields = new IteratorFields<A>(in head, tail);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<A>> lazy) =>
        fields = new IteratorFields<A>(lazy);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, object? ta, VirtualTable<A>? vt, in Space128 space) =>
        fields = new IteratorFields<A>(in head, ta, vt, in space);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<A>> init, in A last) =>
        fields = new IteratorFields<A>(init, in last);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Iterator<A> init, in A last) =>
        fields = new IteratorFields<A>(init, in last);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return fields.tag == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Cons<A> cons)
    {
        if(TryGetValue(out var h, out var t))
        {
            cons = new Cons<A>(in h, in t);
            return true;
        }
        else
        {
            cons = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out A head, out Iterator<A> tail) =>
        fields.tag switch
        {
            IteratorTag.Iterable  => IterableCase(out head, out tail),
            IteratorTag.Singleton => SingletonCase(out head, out tail),
            IteratorTag.Cons      => ConsCase(out head, out tail),
            IteratorTag.Lazy      => LazyCase(out head, out tail),
            IteratorTag.Add       => AddCase(out head, out tail),
            _                     => EmptyCase(out head, out tail)
        };

    public Iterator<A> Lower
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => this;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool IterableCase(out A head, out Iterator<A> tail)
    {
        tail = this;        // Copy
        head = fields.head;
        fields.vt!.Next(in fields.ta!, ref Unsafe.As<Iterator<A>, IteratorFieldsMutable<A>>(ref tail));
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool EmptyCase(out A head, out Iterator<A> tail)
    {
        head = default!;
        tail = default!;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool SingletonCase(out A head, out Iterator<A> tail)
    {
        head = fields.head;
        tail = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool ConsCase(out A head, out Iterator<A> tail)
    {
        head = fields.head;
        tail = fields.lazy!();
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool LazyCase(out A head, out Iterator<A> tail)
    {
        return fields.lazy!().TryGetValue(out head, out tail);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool AddCase(out A head, out Iterator<A> tail)
    {
        var first = fields.lazy!();
        if (first.TryGetValue(out head, out var nt))
        {
            tail = new Iterator<A>(nt, fields.head);
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
        TryGetValue(out Cons<A> cons)
            ? cons
            : new Nil();
    
    public IteratorEnumerator<A> GetEnumerator() => 
        new(this);

    public static Iterator<A> operator +(in A head, in Iterator<A> tail) =>
        new (in head, tail);

    public static Iterator<A> operator +(in Iterator<A> first, in A next) =>
        new (first, in next);    
    
    public static implicit operator Iterator<A> (Nil nil) =>
        default;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    // LINQ operators
    // 

    /// <summary>
    /// Projects each element of a range into a new form.
    /// </summary>
    public Iterator<B> Select<B>(Func<A, B> f)
    {
        switch (fields.tag)
        {
            case IteratorTag.Singleton:
                return new Iterator<B>(f(fields.head));

            case IteratorTag.Cons:
            {
                var tail = fields.lazy ?? (() => default);
                return new Iterator<B>(f(fields.head), () => tail().Select(f));
            }

            case IteratorTag.Lazy:
            {
                var iter = fields.lazy ?? (() => default);
                return new Iterator<B>(() => iter().Select(f));
            }

            case IteratorTag.Iterable:
            {
                var s = this;
                var t = s;        // Copy
                var h = s.fields.head;
                s.fields.vt!.Next(in s.fields.ta!, ref Unsafe.As<Iterator<A>, IteratorFieldsMutable<A>>(ref t));
                return new Iterator<B>(f(h), () => t.Select(f));
            }

            case IteratorTag.Add:
            {
                var tail = fields.lazy ?? (() => default);
                return new Iterator<B>(() => tail().Select(f), f(fields.head));
            }
            
            default:
                return default;
        }
    }
}
