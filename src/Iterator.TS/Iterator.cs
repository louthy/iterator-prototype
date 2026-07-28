using System.Runtime.CompilerServices;
using IteratorTest.Traits;
using LanguageExt.Traits;

namespace IteratorTest;

[Union]
public readonly struct Iterator<T, TS, A> : IUnion
    where T : IterableK<T, TS>
    where TS : struct
{
    readonly IteratorTag tag;
    readonly A head;
    readonly K<T, A>? ta;
    readonly Func<Iterator<T, TS, A>>? lazy;
    readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    readonly TS space;

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
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(in A one)
    {
        tag = IteratorTag.Singleton;
        head = one;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Func<Iterator<T, TS, A>> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, Iterator<T, TS, A> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = () => tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(Func<Iterator<T, TS, A>> lazy)
    {
        tag = IteratorTag.Lazy;
        head = default!;
        this.lazy = lazy;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(in A head, K<T, A> source, in TS state)
    {
        tag = IteratorTag.IterableK;
        this.head = head;
        ta = source;
        vt = VirtualTableCache<T, TS, A>.Cache;
        space = state;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator(Iterator<T, TS, A> first, in A then)
    {
        tag = IteratorTag.Add;
        head = then;
        lazy = () => first;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal Iterator(K<T, A> src, in TS state, out IteratorTag tag)
    {
        ta = src;
        space = state;
        vt = VirtualTableCache<T, TS, A>.Cache;
        tag = T.Step(ta, ref space, out head)
                  ? IteratorTag.IterableK
                  : IteratorTag.Empty;
        this.tag = tag;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Nil nil)
    {
        nil = default;
        return tag == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out Cons<T, TS, A> cons)
    {
        if(TryGetValue(out var h, out var t))
        {
            cons = new Cons<T, TS, A>(in h, in t);
            return true;
        }
        else
        {
            cons = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out A h, out Iterator<T, TS, A> t)
    {
        switch (tag)
        {
            case IteratorTag.IterableK:
                ref readonly var s = ref space;
                h = head;
                t = new Iterator<T, TS, A>(ta!, in s, out var tg);
                return tg == IteratorTag.IterableK;

                /*
                if (T.Step(ta!, ref s, out var nh))
                {
                    t = new Iterator<T, TS, A>(in nh, ta!, in s);
                    return true;
                }
                else
                {
                    t = default;
                    return true;
                }
                */
            
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
                    t = new Iterator<T, TS, A>(nt, head);
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
        TryGetValue(out Cons<T, TS, A> cons)
            ? cons
            : new Nil();
    
    public IteratorEnumerator<T, TS, A> GetEnumerator() => 
        new(this);

    public static Iterator<T, TS, A> operator +(in A head, in Iterator<T, TS, A> tail) =>
        new (head, tail);

    public static Iterator<T, TS, A> operator +(in Iterator<T, TS, A> first, in A next) =>
        new (first, next);
}
