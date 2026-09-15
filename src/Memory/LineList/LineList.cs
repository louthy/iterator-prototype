using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
readonly struct LineList<A> : IDisposable
{
    readonly LineRef<A> line;   // 8 bytes (managed-reference)
    readonly int count;         // 4 bytes

    [MethodImpl(Optimisations.InliningOnly)]
    internal LineList(LineRef<A> line, int count)
    {
        this.line = line;
        this.count = count;
    }
    
    public int Count
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Line<A>.Enumerator GetEnumerator() =>
        Line.GetEnumerator(count);

    [MethodImpl(Optimisations.InliningOnly)]
    public Line<A>.Enumerator GetEnumerator(int take) =>
        take == 0
            ? Line.GetEnumerator(0)
            : Line.GetEnumerator(Math.Min(take, count));

    [MethodImpl(Optimisations.InliningOnly)]
    public Line<A>.Enumerator GetEnumerator(int skip, int take) =>
        skip >= count
            ? Line.GetEnumerator(0)
            : Line.GetEnumerator(skip, Math.Min(take, count - skip));

    [MethodImpl(Optimisations.InliningOnly)]
    public static LineList<A> Alloc() =>
        new (Lines<A>.alwaysAlloc(), 0);

    [MethodImpl(Optimisations.InliningOnly)]
    public static LineList<A> Alloc(params ReadOnlySpan<A> values)
    {
        var line = Lines<A>.alwaysAlloc();
        values.CopyTo(line.AsSpan());
        return new(line, values.Length);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Dispose()
    {
        if(line is not null)
        {
            Line.Dispose();
            Unsafe.AsRef(in line) = null!;
        }
    }
    
    public bool IsAllocated
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => line is not null && line.IsAllocated;
    }
    
    public bool IsDisposed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => line is null || !line.IsAllocated;
    }
    
    public bool IsFullOrDisposed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => line is null || count >= line.Capacity;
    }
    
    public bool IsFull
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count >= Line.Capacity;
    }
    
    internal LineRef<A> Line
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => line ?? throw new ObjectDisposedException(nameof(LineList<>));
    }

    public ref A this[int index] => 
        ref At(index);

    public ref A this[Index index] => 
        ref At(index);

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A At(int index) =>
        ref Line[index];

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A At(Index index) =>
        ref Line[index.GetOffset(Count)];

    [MethodImpl(Optimisations.InliningOnly)]
    public bool At(int index, out A value)
    {
        if (line is null || index >= count || !Line.IsAllocated)
        {
            value = default!;
            return false;
        }
        else
        {
            value = line[index];
            return true;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool At(Index index, out A value)
    {
        var ix = index.GetOffset(Count);
        if (line is null || ix >= count || !line.IsAllocated)
        {
            value = default!;
            return false;
        }
        else
        {
            value = line[ix];
            return true;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool CopyTo(out LineList<A> newList)
    {
        var newLine = Lines<A>.alwaysAlloc();
        Line.CopyDataTo(newLine, count);
        newList = new LineList<A>(newLine, count);
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
    public bool Add(in A value, out LineList<A> newList)
    {
        if (line is null || count >= line.Capacity || !line.IsAllocated)
        {
            var newLine = Lines<A>.alwaysAlloc();
            newLine[0] = value;
            newList = new LineList<A>(newLine, 1);
            return false;
        }

        var ncount = count + 1;
        if (line.SetVersion(count, ncount))
        {
            ref var entry = ref line[count];
            entry = value;
            newList = new LineList<A>(line, ncount);
            return true;
        }
        else
        {
            var newLine = Lines<A>.alwaysAlloc();
            line.CopyDataTo(newLine);
            newLine.SetVersion(0, ncount);
            ref var newEntry = ref newLine[count];
            newEntry = value;
            newList = new LineList<A>(newLine, ncount);
            return false;
        }
    }

    public override string ToString()
    {
        if (IsDisposed) return "[] : disposed";
        if (count == 0) return "[]";
        var sm = new StringMaker(stackalloc char[4096]);
        sm.Append("[");
        sm.Append(Line[0]);
        for (var i = 1; i < count; i++)
        {
            sm.Append(", ");
            sm.Append(Line[i]);
        }
        sm.Append("]");
        return sm.ToString();
    }
    
    public void AppendToStringMaker(ref StringMaker sm)
    {
        if (!IsAllocated || count <= 0) return;
        for (var i = 0; i < count; i++)
        {
            sm.Append(Line[i]);
            sm.Append(", ");
        }
    }
}