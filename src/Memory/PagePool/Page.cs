using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

public abstract class Page<A>
{
    public MetaData32 MetaData;                     // 32 bytes    
    public Version Version;                         // 4 bytes
    public abstract int Capacity { get; }
    protected abstract ref A Reference { get; }

    public ref A this[int index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.Add(ref Reference, index);
    }

    public ref A this[uint index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.Add(ref Reference, index);
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan() =>
        MemoryMarshal.CreateSpan(ref Reference, Capacity);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Enumerator GetEnumerator() => 
        new (this, 0, Capacity);

    [MethodImpl(Optimisations.InliningOnly)]
    public Enumerator GetEnumerator(int take) =>
        take == 0
            ? new(this, 0, 0)
            : new(this, 0, Math.Min(take, Math.Min(take, Capacity)));

    [MethodImpl(Optimisations.InliningOnly)]
    public Enumerator GetEnumerator(int skip, int take) =>
        skip >= Capacity
            ? new(this, 0, 0)
            : new(this, skip, Math.Min(take, Capacity - skip));

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(int start)
    {
        var capacity = Capacity;
        start = Math.Min(start, capacity);
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Reference, start), capacity - start);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(uint start)
    {
        var capacity = (uint)Capacity;
        start = Math.Min(start, capacity);
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Reference, start), (int)(capacity - start));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(int start, int count)
    {
        var capacity = Capacity;
        start = Math.Min(start, capacity);
        count = Math.Min(count, capacity - start);
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Reference, start), count);
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(uint start, uint count)
    {
        var capacity = (uint)Capacity;
        start = Math.Min(start, capacity);
        count = Math.Min(count, capacity - start);
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Reference, start), (int)count);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyTo(Page<A> dest) =>
        CopyTo(dest, Capacity);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyTo(Page<A> dest, int count)
    {
        Avx.CopyAligned(in MetaData, ref dest.MetaData);
        dest.Version = Version;
        if (count > 0)
        {
            Avx.CopyAligned(in Reference, ref dest.Reference, count);
        }
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyDataTo(Page<A> dest) =>
        CopyDataTo(dest, Capacity);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyDataTo(Page<A> dest, int count)
    {
        if (count > 0)
        {
            Avx.CopyAligned(in Reference, ref dest.Reference, count);
        }
    }
    
    public ref struct Enumerator : IEnumerator<A>
    {
        readonly ref A first;
        readonly ref A last;
        ref A item;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(Page<A> Page, int start, int count)
        {
            first = ref Unsafe.Add(ref Page.Reference, start - 1);
            last = ref Unsafe.Add(ref Page.Reference, start + count);
            item = ref first;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            item = ref Unsafe.Add(ref item, 1);
            return !Unsafe.AreSame(ref last, ref Unsafe.NullRef<A>()) &&    // < Makes sure this is a non-default struct 
                   !Unsafe.AreSame(ref item, ref last);
        }

        public ref A Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref item;
        }

        A IEnumerator<A>.Current => 
            Current;

        object IEnumerator.Current => 
            Current!;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() =>
            item = ref first;

        void IDisposable.Dispose() { }
    }
}

public abstract class Page<Self, A> : Page<A> 
    where Self : Page<Self, A>
{
    // Wrapper to avoid covariance issues
    [SkipLocalsInit]
    public struct Entry
    {
        public A Item;
 
        public override string ToString() =>
            Item?.ToString() ?? "[null]";
    }
}
