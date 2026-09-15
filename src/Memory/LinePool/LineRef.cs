using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

public sealed class LineRef<A> : IDisposable
{
    // ReSharper disable once ReplaceWithFieldKeyword
    readonly Line<A> line;    // 8 bytes (managed-reference)                  
    LineId lineId;            // 4 bytes (value-type)
    
    public static readonly LineRef<A> None = new (LineId.None, null!);
    
    [MethodImpl(Optimisations.InliningOnly)]
    internal LineRef(in LineId lineId, in Line<A> line)
    {
        this.lineId = lineId;
        this.line = line;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    ~LineRef() =>
        Free();

    [MethodImpl(Optimisations.InliningOnly)]
    public void Dispose() =>
        Free();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void Free() =>
        Lines<A>.free(this);

    [MethodImpl(Optimisations.InliningOnly)]
    internal void SetLineId(in LineId newLineId) =>
        lineId = newLineId;

    [MethodImpl(Optimisations.InliningOnly)]
    internal bool IfAllocatedChangeToIndexed()
    {
        ref var bid = ref Unsafe.AsRef(in lineId);
        return bid.IfAllocatedChangeToIndexed();
    }

    public Version Version
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => Line.Version;
    }

    [MethodImpl(Optimisations.Max)]
    public bool SetVersion(Version expectedCurrentVersion, Version newVersion)
    {
        ref var version = ref Line.Version.Raw;
        var     current = (int)expectedCurrentVersion;
        var     next    = (int)newVersion;
        return Interlocked.CompareExchange(ref version, next, current) == current;
    }
    
    public int Capacity
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => Line.Capacity;
    }

    public bool IsNone
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => LineId.IsNone;
    }

    public bool IsAllocated
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => LineId.IsAllocated;
    }

    public bool IsIndexed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => LineId.IsIndexed;
    }

    internal Line<A> Line
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => IsAllocated
                   ? Line
                   : throw new ObjectDisposedException(nameof(LineRef<>));
    }

    public LineId LineId 
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => lineId;
    }
    
    public ref A this[int index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Line[index];
    }
    
    public ref A this[Index index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Line[index.GetOffset(Capacity)];
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan() =>
        Line.AsSpan();

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(int start) =>
        Line.AsSpan(start);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(int start, int count) =>
        Line.AsSpan(start, count);

    [MethodImpl(Optimisations.InliningOnly)]
    public Line<A>.Enumerator GetEnumerator() =>
        Line.GetEnumerator();    

    [MethodImpl(Optimisations.InliningOnly)]
    public Line<A>.Enumerator GetEnumerator(int take) =>
        Line.GetEnumerator(take);    

    [MethodImpl(Optimisations.InliningOnly)]
    public Line<A>.Enumerator GetEnumerator(int skip, int take) =>
        Line.GetEnumerator(skip, take);    

    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyTo(LineRef<A> dest) =>
        Line.CopyTo(dest.Line);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyTo(LineRef<A> dest, int count) =>
        Line.CopyTo(dest.Line, count);
        
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyDataTo(LineRef<A> dest) =>
        Line.CopyDataTo(dest.Line);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyDataTo(LineRef<A> dest, int count) =>
        Line.CopyDataTo(dest.Line, count);

    public override string ToString() =>
        LineId.ToString();
}
