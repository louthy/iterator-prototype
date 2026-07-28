using System.Runtime.CompilerServices;

namespace IteratorTest;

[Union]
public readonly struct Iterator<A> : IUnion
{
    readonly int tag;
    readonly A head;
    readonly object? obj1;
    readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    readonly Space128 space;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in Nil nil)
    {
        tag = 0;
        head = default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A one)
    {
        tag = 1;
        head = one;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A head, Func<Iterator<A>> tail)
    {
        tag = 2;
        this.head = head;
        obj1 = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A head, in Iterator<A> tail)
    {
        tag = 3;
        this.head = head;
        obj1 = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(Func<Iterator<A>> lazy)
    {
        tag = 4;
        head = default!;
        obj1 = lazy;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A one, object? ta, VirtualTable<A>? vt, in Space128 state)
    {
        tag = 5;
        head = default!;
        obj1 = ta;
        space = state;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in Iterator<A> first, in A then)
    {
        tag = 6;
        head = then;
        obj1 = first;
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
            case 1:
                h = head;
                t = default;
                return true;
            
            case 2:
                h = head;
                t = ((Func<Iterator<A>>)obj1!)();
                return true;
            
            case 3:
                h = head;
                t = (Iterator<A>)obj1!;
                return true;

            case 4:
                return ((Func<Iterator<A>>)obj1!)().TryGetValue(out h, out t);

            case 5:
                var s = space;
                h = head;
                if (vt!.Step(obj1!, ref s, out t))
                {
                    return true;
                }
                else
                {
                    t = default;
                    return true;
                }                

            case 6:
                var first = (Iterator<A>)obj1!;
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
        get => tag is >= 0 and <= 5;
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
