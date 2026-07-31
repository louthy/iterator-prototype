using System.Runtime.CompilerServices;

namespace IteratorTest;

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
public readonly struct Iterator<A> : IUnion
{
    internal readonly IteratorTag tag;
    internal readonly A head;
    internal readonly object? ta;
    internal readonly Func<Iterator<A>>? lazy;
    internal readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    internal readonly Space128 space;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in Nil nil)
    {
        tag = IteratorTag.Empty;
        head = default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A one)
    {
        tag = IteratorTag.Singleton;
        head = one;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A head, Func<Iterator<A>> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A head, Iterator<A> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = () => tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(Func<Iterator<A>> lazy)
    {
        tag = IteratorTag.Lazy;
        head = default!;
        this.lazy = lazy;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, object? ta, VirtualTable<A>? vt, in Space128 space)
    {
        tag = IteratorTag.IterableK;
        this.head = head;
        this.ta = ta;
        this.vt = vt;
        this.space = space;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(Iterator<A> first, in A then)
    {
        tag = IteratorTag.Add;
        head = then;
        lazy = () => first;
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
    public bool TryGetValue(out A head, out Iterator<A> tail)
    {
        switch (tag)
        {
            case IteratorTag.IterableK:
                tail = this;        // Copy
                head = this.head;
                vt!.Next(ta!, ref Unsafe.As<Iterator<A>, IteratorMutable<A>>(ref tail));
                return true;

            case IteratorTag.Empty:
                head = default!;
                tail = default!;
                return false;
            
            case IteratorTag.Singleton:
                head = this.head;
                tail = default;
                return true;
            
            case IteratorTag.Cons:
                head = this.head;
                tail = lazy!();
                return true;

            case IteratorTag.Lazy:
                return lazy!().TryGetValue(out head, out tail);

            case IteratorTag.Add:
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
            
            default:
                head = default!;
                tail = default!;
                return false;
        }
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
        new (head, tail);

    public static Iterator<A> operator +(in Iterator<A> first, in A next) =>
        new (first, next);    
}
