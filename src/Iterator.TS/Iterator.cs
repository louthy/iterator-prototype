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
    public bool TryGetValue(out A head, out Iterator<T, TS, A> tail)
    {
        switch (tag)
        {
            case IteratorTag.IterableK:
                head = this.head;
                return T.StepImmutable(ta!, in space, out tail);
            
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
                    tail = new Iterator<T, TS, A>(nt, this.head);
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
