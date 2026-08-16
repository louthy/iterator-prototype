// ReSharper disable ParameterHidesMember
using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct IteratorMutable<A>
{
    // MUST MATCH THE FIELDS IN Iterator<T, TS, A>
    public IteratorTag tag;
    public A head;
    public object? ta;
    public Func<Iterator<A>>? lazy;
    public VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public Space128 space;
}

[Union]
[SkipLocalsInit]
public readonly struct Iterator<A> : IUnion, IIterator<Iterator<A>, A>, K<Iterator, A>
{
    readonly IteratorTag tag;
    readonly A head;
    readonly object? ta;
    readonly Func<Iterator<A>>? lazy;
    readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    readonly Space128 space;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in Nil nil)
    {
        tag = IteratorTag.Empty;
        head = default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A one)
    {
        tag = IteratorTag.Singleton;
        head = one;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Func<Iterator<A>> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Iterator<A> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = () => tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<A>> lazy)
    {
        tag = IteratorTag.Lazy;
        head = default!;
        this.lazy = lazy;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, object? ta, VirtualTable<A>? vt, in Space128 space)
    {
        tag = IteratorTag.Iterable;
        this.head = head;
        this.ta = ta;
        this.vt = vt;
        this.space = space;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<A>> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        lazy = init;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Iterator<A> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        lazy = () => init;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return tag == 0;
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
        tag switch
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
        head = this.head;
        vt!.Next(in ta!, ref Unsafe.As<Iterator<A>, IteratorMutable<A>>(ref tail));
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
        head = this.head;
        tail = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool ConsCase(out A head, out Iterator<A> tail)
    {
        head = this.head;
        tail = lazy!();
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool LazyCase(out A head, out Iterator<A> tail)
    {
        return lazy!().TryGetValue(out head, out tail);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool AddCase(out A head, out Iterator<A> tail)
    {
        var first = lazy!();
        if (first.TryGetValue(out head, out var nt))
        {
            tail = new Iterator<A>(nt, this.head);
        }
        else
        {
            head = this.head;
            tail = default;
        }
        return true;
    }

    public bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => tag is >= IteratorTag.Empty and < IteratorTag.MaxValue;
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
        switch (tag)
        {
            case IteratorTag.Singleton:
                return new Iterator<B>(f(head));

            case IteratorTag.Cons:
            {
                var tail = lazy ?? (() => default);
                return new Iterator<B>(f(head), () => tail().Select(f));
            }

            case IteratorTag.Lazy:
            {
                var iter = lazy ?? (() => default);
                return new Iterator<B>(() => iter().Select(f));
            }

            case IteratorTag.Iterable:
            {
                var s = this;
                var t = s;        // Copy
                var h = s.head;
                s.vt!.Next(in s.ta!, ref Unsafe.As<Iterator<A>, IteratorMutable<A>>(ref t));
                return new Iterator<B>(f(h), () => t.Select(f));
            }

            case IteratorTag.Add:
            {
                var tail = lazy ?? (() => default);
                return new Iterator<B>(() => tail().Select(f), f(head));
            }
            
            default:
                return default;
        }
    }
}
