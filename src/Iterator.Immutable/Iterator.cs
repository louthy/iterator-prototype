// ReSharper disable ParameterHidesMember
// ReSharper disable NotAccessedField.Local
using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

[SkipLocalsInit]
public ref struct IteratorMutable<TA, IS, A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    // MUST MATCH THE FIELDS IN Iterator<T, TS, A>
    public IteratorTag tag;
    public A head;
    public TA ta;
    public Func<Iterator<TA, IS, A>>? lazy;
    public VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public IS space;
}

[Union]
[SkipLocalsInit]
public readonly struct Iterator<TA, IS, A> : IUnion, IIterator<Iterator<TA, IS, A>, A>
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct
{
    readonly IteratorTag tag;
    readonly A head;
    readonly TA ta;
    readonly Func<Iterator<TA, IS, A>>? lazy;
    readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    readonly IS space;

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => tag == IteratorTag.Empty;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in Nil nil)
    {
        tag = 0;
        head = default!;
        ta = null!;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A one)
    {
        tag = IteratorTag.Singleton;
        head = one;
        ta = null!;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Func<Iterator<TA, IS, A>> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        ta = null!;
        lazy = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Iterator<TA, IS, A> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        ta = null!;
        lazy = () => tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Func<Iterator<TA, IS, A>> lazy)
    {
        tag = IteratorTag.Lazy;
        head = default!;
        ta = null!;
        this.lazy = lazy;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, in TA source, in IS state)
    {
        tag = IteratorTag.IterableK;
        this.head = head;
        ta = source;
        vt = VirtualTableCache<TA, IS, A>.Cache;
        space = state;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(Iterator<TA, IS, A> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        ta = null!;
        lazy = () => init;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return tag == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Cons<TA, IS, A> cons)
    {
        if(TryGetValue(out var h, out var t))
        {
            cons = new Cons<TA, IS, A>(in h, in t);
            return true;
        }
        else
        {
            cons = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out A head, out Iterator<TA, IS, A> tail)
    {
        switch (tag)
        {
            case IteratorTag.IterableK:
            {
                tail = this; // Copy
                head = this.head;
                TA.NextImmutable(in ta!, ref Unsafe.As<Iterator<TA, IS, A>, IteratorMutable<TA, IS, A>>(ref tail));
                return true;
            }

            case IteratorTag.Empty:
            {
                head = default!;
                tail = default!;
                return false;
            }

            case IteratorTag.Singleton:
            {
                head = this.head;
                tail = default;
                return true;
            }

            case IteratorTag.Cons:
            {
                head = this.head;
                tail = lazy!();
                return true;
            }

            case IteratorTag.Lazy:
            {
                return lazy!().TryGetValue(out head, out tail);
            }

            case IteratorTag.Add:
            {
                var first = lazy!();
                if (first.TryGetValue(out head, out var nt))
                {
                    tail = new Iterator<TA, IS, A>(nt, this.head);
                }
                else
                {
                    head = this.head;
                    tail = default;
                }

                return true;
            }

            default:
            {
                head = default!;
                tail = default!;
                return false;
            }
        }
    }

    public bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => tag is >= IteratorTag.Empty and < IteratorTag.MaxValue;
    }

    public object? Value =>
        TryGetValue(out Cons<TA, IS, A> cons)
            ? cons
            : new Nil();
    
    public IteratorEnumerator<TA, IS, A> GetEnumerator() => 
        new(this);

    public static Iterator<TA, IS, A> operator +(in A head, in Iterator<TA, IS, A> tail) =>
        new (head, tail);

    public static Iterator<TA, IS, A> operator +(in Iterator<TA, IS, A> first, in A next) =>
        new (first, next);
    
    public static implicit operator Iterator<TA, IS, A> (Nil nil) =>
        default;

    public static implicit operator Iterator<A>(Iterator<TA, IS, A> iter) =>
        Unsafe.As<Iterator<TA, IS, A>, Iterator<A>>(ref iter);
}
