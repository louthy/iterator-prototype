using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
readonly struct PageList<A> : IDisposable
{
    readonly PageRef<A> page;     // 8 bytes (managed-reference)
    readonly Version count; // 4 bytes

    [MethodImpl(Optimisations.InliningOnly)]
    PageList(PageRef<A> page)
    {
        this.page = page;
        count = page.Version;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    internal PageList(PageRef<A> page, Version count)
    {
        this.page = page;
        this.count = count;
    }
    
    public int Count
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Page<A>.Enumerator GetEnumerator() =>
        Page.GetEnumerator(count);

    [MethodImpl(Optimisations.InliningOnly)]
    public Page<A>.Enumerator GetEnumerator(int take) =>
        take == 0
            ? Page.GetEnumerator(0)
            : Page.GetEnumerator(Math.Min(take, count));

    [MethodImpl(Optimisations.InliningOnly)]
    public Page<A>.Enumerator GetEnumerator(int skip, int take) =>
        skip >= count
            ? Page.GetEnumerator(0)
            : Page.GetEnumerator(skip, Math.Min(take, count - skip));

    [MethodImpl(Optimisations.InliningOnly)]
    public static PageList<A> Alloc() =>
        new (Pages<A>.alwaysAlloc());

    [MethodImpl(Optimisations.InliningOnly)]
    public static PageList<A> Alloc(params ReadOnlySpan<A> values)
    {
        var b = Pages<A>.alwaysAlloc();
        values.CopyTo(b.AsSpan());
        return new(b);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Dispose()
    {
        if(page is not null)
        {
            Page.Dispose();
            Unsafe.AsRef(in page) = null!;
        }
    }
    
    public bool IsAllocated
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => page is not null && page.IsAllocated;
    }
    
    public bool IsDisposed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => page is null || !page.IsAllocated;
    }
    
    public bool IsFullOrDisposed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => page is null || count >= page.Capacity;
    }
    
    public bool IsFull
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count >= Page.Capacity;
    }
    
    internal PageRef<A> Page
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => page ?? throw new ObjectDisposedException(nameof(PageList<>));
    }

    public ref A this[int index] => 
        ref At(index);

    public ref A this[Index index] => 
        ref At(index);

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A At(int index) =>
        ref Page[index];

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A At(Index index) =>
        ref Page[index.GetOffset(Count)];

    [MethodImpl(Optimisations.InliningOnly)]
    public bool At(int index, out A value)
    {
        if (page is null || index >= count || !Page.IsAllocated)
        {
            value = default!;
            return false;
        }
        else
        {
            value = page[index];
            return true;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool At(Index index, out A value)
    {
        var ix = index.GetOffset(Count);
        if (page is null || ix >= count || !page.IsAllocated)
        {
            value = default!;
            return false;
        }
        else
        {
            value = page[ix];
            return true;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool CopyTo(out PageList<A> newList)
    {
        var newPage = Pages<A>.alwaysAlloc();
        Page.CopyDataTo(newPage, count);
        newPage.SetVersion(0, count);
        newList = new PageList<A>(newPage);
        return true;
    }
    
    /// <summary>
    /// Ass a new value to the Page.  This will always succeed, but if the Page is full, a
    /// new Page will be allocated and that will be returned in `newList`. The result is `false`
    /// if a new Page was allocated, `true` if we're able to add to the current Page.
    /// </summary>
    /// <param name="value">Value to add</param>
    /// <param name="newList">Updated Page-list</param>
    /// <returns></returns>
    public bool Add(in A value, out PageList<A> newList)
    {
        if (page is null || count >= page.Capacity || !page.IsAllocated)
        {
            var newPage = Pages<A>.alwaysAlloc();
            newPage.SetVersion(0, 1);
            newPage[0] = value;
            newList = new PageList<A>(newPage);
            return false;
        }

        var ncount = count + 1;
        if (page.SetVersion(count, ncount))
        {
            ref var entry = ref page[count];
            entry = value;
            newList = new PageList<A>(page, ncount);
            return true;
        }
        else
        {
            var newPage = Pages<A>.alwaysAlloc();
            page.CopyDataTo(newPage);
            newPage.SetVersion(0, count + 1);
            ref var newEntry = ref newPage[count];
            newEntry = value;
            newList = new PageList<A>(newPage);
            return false;
        }
    }

    public override string ToString()
    {
        if (IsDisposed) return "[] : disposed";
        if (count == 0) return "[]";
        var sm = new StringMaker(stackalloc char[4096]);
        sm.Append("[");
        sm.Append(Page[0]);
        for (var i = 1; i < count; i++)
        {
            sm.Append(", ");
            sm.Append(Page[i]);
        }
        sm.Append("]");
        return sm.ToString();
    }
    
    public void AppendToStringMaker(ref StringMaker sm)
    {
        if (!IsAllocated || count <= 0) return;
        for (var i = 0; i < count; i++)
        {
            sm.Append(Page[i]);
            sm.Append(", ");
        }
    }
}