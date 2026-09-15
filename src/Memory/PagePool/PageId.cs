using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable LocalVariableHidesMember

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = 4)]
public readonly struct PageId : IEquatable<PageId>, IComparable<PageId>
{
    public enum StateFlag : byte
    {
        None = 0,
        Allocated = 1,
        Indexed = 2
    }

    /// <summary>
    /// Represents a Page id that is not valid.
    /// </summary>
    public static readonly PageId None = new (StateFlag.None, 0, 0);
    
    /// <summary>
    /// Represents a Page that is in use and so has either come from a pool or has been allocated
    /// through the standard .NET allocator.
    /// </summary>
    public static readonly PageId Allocated = new (StateFlag.Allocated, 0, 0);
    
    /// <summary>
    /// Processor index.  This is the zero-based index of the processor that owns the Page.
    /// </summary>
    [FieldOffset(0)] 
    public readonly StateFlag State;
    
    /// <summary>
    /// Processor index.  This is the zero-based index of the processor that owns the Page.
    /// </summary>
    [FieldOffset(1)] 
    public readonly byte ProcessorIndex;
    
    /// <summary>
    /// Index of the Page into the index of Pages  
    /// </summary>
    [FieldOffset(2)] 
    public readonly ushort PageIndex;
    
    [MethodImpl(Optimisations.InliningOnly)]
    internal PageId(StateFlag state, int processorIndex, int pageIndex)
    {
        State = state;
        ProcessorIndex = (byte)processorIndex;
        PageIndex = (ushort)pageIndex;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    internal bool IfAllocatedChangeToIndexed()
    {
        var Allocated = (byte)StateFlag.Allocated; 
        var Indexed   = (byte)StateFlag.Indexed; 
        
        ref var byteState = ref Unsafe.As<StateFlag, byte>(ref Unsafe.AsRef(in State));
        return Interlocked.CompareExchange(ref byteState, Indexed, Allocated) == Allocated;
    }
    
    public bool IsNone
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => State is StateFlag.None;
    } 

    public bool IsAllocated
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => State is StateFlag.Allocated;
    } 

    public bool IsIndexed
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => State is StateFlag.Indexed;
    } 
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator ==(PageId left, PageId right) =>
        left.Equals(right);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator !=(PageId left, PageId right) =>
        !left.Equals(right);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >(PageId left, PageId right) =>
        left.CompareTo(right) > 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <(PageId left, PageId right) =>
        left.CompareTo(right) < 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >=(PageId left, PageId right) =>
        left.CompareTo(right) >= 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <=(PageId left, PageId right) =>
        left.CompareTo(right) <= 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Equals(PageId other) =>
        ProcessorIndex == other.ProcessorIndex &&
        PageIndex     == other.PageIndex;

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Equals(object? obj) =>
        obj is PageId other && Equals(other);

    [MethodImpl(Optimisations.InliningOnly)]
    public override int GetHashCode() =>
        HashCode.Combine(ProcessorIndex, PageIndex);

    [MethodImpl(Optimisations.InliningOnly)]
    public int CompareTo(PageId other)
    {
        var processorIndexComparison = ProcessorIndex.CompareTo(other.ProcessorIndex);
        if (processorIndexComparison != 0) return processorIndexComparison;
        return PageIndex.CompareTo(other.PageIndex);
    }

    public override string ToString() =>
        State switch
        {
            StateFlag.None                                => "Page none",
            StateFlag.Indexed                             => $"Page index({ProcessorIndex}:{PageIndex})",
            StateFlag.Allocated when PageIndex == 0xffff => $"Page alloc({ProcessorIndex}:GC)",
            StateFlag.Allocated                           => $"Page alloc({ProcessorIndex}:{PageIndex})",
            
            _ => throw new InvalidOperationException($"Unknown PageId.State: {State}")
        };
}