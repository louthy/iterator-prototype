using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

public sealed class PageRef<A> : IDisposable
{
    // ReSharper disable once ReplaceWithFieldKeyword
    readonly Page<A> page; // 8 bytes ('Page' auto-property managed-reference, defined below)                  
    PageId pageId;         // 4 bytes (value-type)
    
    public static readonly PageRef<A> None = new (PageId.None, null!);
    
    [MethodImpl(Optimisations.InliningOnly)]
    internal PageRef(in PageId pageId, in Page<A> page)
    {
        this.pageId = pageId;
        this.page = page;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    ~PageRef()
    {
        GC.ReRegisterForFinalize(this);
        Free();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Dispose() =>
        Free();

    [MethodImpl(Optimisations.InliningOnly)]
    public void Free()
    {
        Pages<A>.free(this);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    internal void SetPageId(in PageId newPageId) =>
        pageId = newPageId;

    [MethodImpl(Optimisations.InliningOnly)]
    internal bool IfAllocatedChangeToIndexed()
    {
        ref var bid = ref Unsafe.AsRef(in pageId);
        return bid.IfAllocatedChangeToIndexed();
    }

    public Version Version
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => Page.Version;
    }

    [MethodImpl(Optimisations.Max)]
    public bool SetVersion(Version expectedCurrentVersion, Version newVersion)
    {
        ref var version = ref Page.Version.Raw;
        var     current = (int)expectedCurrentVersion;
        var     next    = (int)newVersion;
        return Interlocked.CompareExchange(ref version, next, current) == current;
    }

    public ref MetaData32 MetaDataRaw
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Page.MetaData;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public (Version Version, M Data) MetaData<M>() 
        where M : unmanaged =>
        (Page.Version, Page.MetaData.As<M>());

    [MethodImpl(Optimisations.Max)]
    public bool SetMetaData<M>(Version expectedCurrentVersion, Version newVersion, in M newValue)
        where M : unmanaged
    {
        ref var md = ref Page.MetaData.As<M>();
        if (SetVersion(expectedCurrentVersion, newVersion))
        {
            md = newValue;
            return true;
        }
        else
        {
            return false;
        }
    }

    [MethodImpl(Optimisations.Max)]
    public bool SetMetaData<M>(Version expectedCurrentVersion, M newValue)
        where M : unmanaged
    {
        ref var md = ref Page.MetaData.As<M>();
        if (SetVersion(expectedCurrentVersion, expectedCurrentVersion + 1))
        {
            md = newValue;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public int Capacity
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => Page.Capacity;
    }

    public bool IsNone
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => PageId.IsNone;
    }

    public bool IsAllocated
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => PageId.IsAllocated;
    }

    public bool IsIndexed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => PageId.IsIndexed;
    }

    internal Page<A> Page
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => IsAllocated
                   ? page
                   : throw new ObjectDisposedException(nameof(PageRef<>));
    }

    public PageId PageId 
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => pageId;
    }
    
    public ref A this[int index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Page[index];
    }
    
    public ref A this[uint index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Page[index];
    }
    
    public ref A this[Index index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Page[index.GetOffset(Capacity)];
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan() =>
        Page.AsSpan();

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(int start) =>
        Page.AsSpan(start);

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(uint start) =>
        Page.AsSpan(start);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(int start, int count) =>
        Page.AsSpan(start, count);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<A> AsSpan(uint start, uint count) =>
        Page.AsSpan(start, count);

    [MethodImpl(Optimisations.InliningOnly)]
    public Page<A>.Enumerator GetEnumerator() =>
        Page.GetEnumerator();    

    [MethodImpl(Optimisations.InliningOnly)]
    public Page<A>.Enumerator GetEnumerator(int take) =>
        Page.GetEnumerator(take);    

    [MethodImpl(Optimisations.InliningOnly)]
    public Page<A>.Enumerator GetEnumerator(int skip, int take) =>
        Page.GetEnumerator(skip, take);    

    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyTo(PageRef<A> dest) =>
        Page.CopyTo(dest.Page);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyTo(PageRef<A> dest, int count) =>
        Page.CopyTo(dest.Page, count);
        
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyDataTo(PageRef<A> dest) =>
        Page.CopyDataTo(dest.Page);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void CopyDataTo(PageRef<A> dest, int count) =>
        Page.CopyDataTo(dest.Page, count);

    public override string ToString() =>
        PageId.ToString();
}
