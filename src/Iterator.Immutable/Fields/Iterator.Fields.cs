using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct IteratorFields<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public readonly IteratorTag tag;
    public readonly A head;
    public readonly K<T, A>? ta;
    public readonly Func<Iterator<T, IS, A>>? lazy;
    public readonly VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public readonly IS space;
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in Nil nil)
    {
        tag = IteratorTag.Empty;
        head = default!;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A one)
    {
        tag = IteratorTag.Singleton;
        head = one;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A head, Func<Iterator<T, IS, A>> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = tail;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A head, Iterator<T, IS, A> tail)
    {
        tag = IteratorTag.Cons;
        this.head = head;
        lazy = () => tail;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(Func<Iterator<T, IS, A>> lazy)
    {
        tag = IteratorTag.Lazy;
        head = default!;
        this.lazy = lazy;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(in A head, in K<T, A> ta, in IS space)
    {
        tag = IteratorTag.Iterable;
        this.head = head;
        this.ta = ta;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
        this.space = space;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(Func<Iterator<T, IS, A>> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        lazy = init;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    internal IteratorFields(Iterator<T, IS, A> init, in A last)
    {
        tag = IteratorTag.Add;
        head = last;
        lazy = () => init;
        this.vt = VirtualTableCache<T, IS, A>.Cache;
    }
}