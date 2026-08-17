using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields<A>
{
    public readonly IteratorTag tag;
    public readonly A head;
    public readonly object? ta;
    public readonly Func<Iterator<A>>? lazy;
    public readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public readonly Space128 space;
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in Nil nil)
    {
        tag = IteratorTag.Empty;
        head = default!;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A one)
    {
        tag = IteratorTag.Singleton;
        head = one;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A head, Func<Iterator<A>> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A head, Iterator<A> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = () => tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(Func<Iterator<A>> lazy)
    {
        tag = IteratorTag.Lazy;
        head = default!;
        this.lazy = lazy;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A head, object? ta, VirtualTable<A>? vt, in Space128 space)
    {
        tag = IteratorTag.Iterable;
        this.head = head;
        this.ta = ta;
        this.vt = vt;
        this.space = space;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(Func<Iterator<A>> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        lazy = init;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(Iterator<A> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        lazy = () => init;
    }
}