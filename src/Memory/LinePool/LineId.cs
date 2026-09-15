using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable LocalVariableHidesMember

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = 4)]
public readonly struct LineId : IEquatable<LineId>, IComparable<LineId>
{
    public enum StateFlag : byte
    {
        None = 0,
        Allocated = 1,
        Indexed = 2
    }

    /// <summary>
    /// Represents a Line id that is not valid.
    /// </summary>
    public static readonly LineId None = new (StateFlag.None, 0, 0);
    
    /// <summary>
    /// Represents a Line that is in use and so has either come from a pool or has been allocated
    /// through the standard .NET allocator.
    /// </summary>
    public static readonly LineId Allocated = new (StateFlag.Allocated, 0, 0);
    
    /// <summary>
    /// Processor index.  This is the zero-based index of the processor that owns the Line.
    /// </summary>
    [FieldOffset(0)] 
    public readonly StateFlag State;
    
    /// <summary>
    /// Processor index.  This is the zero-based index of the processor that owns the Line.
    /// </summary>
    [FieldOffset(1)] 
    public readonly byte ProcessorIndex;
    
    /// <summary>
    /// Index of the Line into the index of Lines  
    /// </summary>
    [FieldOffset(2)] 
    public readonly ushort LineIndex;
    
    [MethodImpl(Optimisations.InliningOnly)]
    internal LineId(StateFlag state, int processorIndex, int lineIndex)
    {
        State = state;
        ProcessorIndex = (byte)processorIndex;
        LineIndex = (ushort)lineIndex;
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
    public static bool operator ==(LineId left, LineId right) =>
        left.Equals(right);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator !=(LineId left, LineId right) =>
        !left.Equals(right);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >(LineId left, LineId right) =>
        left.CompareTo(right) > 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <(LineId left, LineId right) =>
        left.CompareTo(right) < 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >=(LineId left, LineId right) =>
        left.CompareTo(right) >= 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <=(LineId left, LineId right) =>
        left.CompareTo(right) <= 0;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Equals(LineId other) =>
        ProcessorIndex == other.ProcessorIndex &&
        LineIndex      == other.LineIndex;

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Equals(object? obj) =>
        obj is LineId other && Equals(other);

    [MethodImpl(Optimisations.InliningOnly)]
    public override int GetHashCode() =>
        HashCode.Combine(ProcessorIndex, LineIndex);

    [MethodImpl(Optimisations.InliningOnly)]
    public int CompareTo(LineId other)
    {
        var processorIndexComparison = ProcessorIndex.CompareTo(other.ProcessorIndex);
        if (processorIndexComparison != 0) return processorIndexComparison;
        return LineIndex.CompareTo(other.LineIndex);
    }

    public override string ToString() =>
        State switch
        {
            StateFlag.None                               => "Line none",
            StateFlag.Indexed                            => $"Line index({ProcessorIndex}:{LineIndex})",
            StateFlag.Allocated when LineIndex == 0xffff => $"Line alloc({ProcessorIndex}:GC)",
            StateFlag.Allocated                          => $"Line alloc({ProcessorIndex}:{LineIndex})",
            
            _ => throw new InvalidOperationException($"Unknown LineId.State: {State}")
        };
}