using System.Runtime.CompilerServices;

namespace IteratorTest;

[Union]
public readonly struct Iterator<A> : IUnion
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
    Iterator(in A one, object? ta, VirtualTable<A>? vt, in Space128 state)
    {
        tag = IteratorTag.IterableK;
        head = default!;
        this.ta = ta;
        space = state;
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
    public bool TryGetValue(out A h, out Iterator<A> t)
    {
        switch (tag)
        {
            case IteratorTag.IterableK:
                ref readonly var s = ref space;
                h = head;
                if (vt!.Step(ta!, in s, out t))
                {
                    return true;
                }
                else
                {
                    t = default;
                    return true;
                }                

            case IteratorTag.Empty:
                h = default!;
                t = default!;
                return false;
            
            case IteratorTag.Singleton:
                h = head;
                t = default;
                return true;
            
            case IteratorTag.Cons:
                h = head;
                t = lazy!();
                return true;

            case IteratorTag.Lazy:
                return lazy!().TryGetValue(out h, out t);

            case IteratorTag.Add:
                var first = lazy!();
                if (first.TryGetValue(out h, out var nt))
                {
                    t = new Iterator<A>(nt, head);
                }
                else
                {
                    h = head;
                    t = default;
                }
                return true;
            
            default:
                h = default!;
                t = default!;
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
